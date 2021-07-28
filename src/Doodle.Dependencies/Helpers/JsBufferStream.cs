using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Doodle.Dependencies.Helpers
{
    public class JsBufferStream : Stream
    {

        #region Members
        private readonly IJSObjectReference _jsObject;
        private readonly int _maxAllowedLength;
        private readonly string _jsBufferName;
        private int _currentIndex = 0;
        private bool _previouslyReadFullBuffer = false;
        
        #endregion

        #region ctor
        public JsBufferStream(IJSObjectReference jsObject, string jsBufferName, int maxAllowedLength = 512 * 1024)
        {
            this._jsObject = jsObject ?? throw new ArgumentNullException(nameof(jsObject));
            this._maxAllowedLength = maxAllowedLength;
            this._jsBufferName = jsBufferName;
        }
        #endregion

        #region Methods
        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => this._currentIndex; set => throw new NotSupportedException(); }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        #endregion

        #region Js Interop Read
        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            if (this._currentIndex > 0 && !this._previouslyReadFullBuffer)
            {
                // If we did not read a full buffer in the previous read, we must be done. There is no reason
                // for the JS to send us partial buffers. Dispose of the Js Memory object.
                await this._jsObject.InvokeVoidAsync("ClearBuffer", this._jsBufferName);
                return 0;
            }

            // This is really bad, need to find a way to transfer a byte array from the JsInterop
            // When there is time look at the Unmarshalled JsIntrop or the getStream example
            // on github.
            var byteString = await this._jsObject.InvokeAsync<string>("ReadBuffer", this._jsBufferName, this._currentIndex);
            if (!string.IsNullOrEmpty(byteString))
            {
                var bytes = (byteString).Split(',').Select<string, int>(int.Parse).Select(x => (byte)x).ToArray();
                
                if (this._maxAllowedLength - this._currentIndex <  bytes.Length)
                {
                    throw new InvalidDataException("Too many bytes read.");
                }

                // Keep this in sync with JS
                const int SegmentSize = 5 * 1024;
                this._previouslyReadFullBuffer = bytes.Length == SegmentSize;

                // Copy the bytes to the parameter buffer
                bytes.AsMemory().CopyTo(buffer);
                this._currentIndex += bytes.Length;
                return bytes.Length;
            }

            return 0;
        }
        #endregion

    }
}