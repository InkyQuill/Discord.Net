﻿using System;
using System.Collections.Concurrent;

namespace Discord.Audio.Streams
{
    ///<summary> Converts Opus to PCM </summary>
    public class OpusDecodeStream : AudioInStream
    {
        private readonly BlockingCollection<byte[]> _queuedData; //TODO: Replace with max-length ring buffer
        private readonly byte[] _buffer;
        private readonly OpusDecoder _decoder;

        internal OpusDecodeStream(AudioClient audioClient, int samplingRate, int channels = OpusConverter.MaxChannels, int bufferSize = 4000)
        {
            _buffer = new byte[bufferSize];
            _decoder = new OpusDecoder(samplingRate, channels);
            _queuedData = new BlockingCollection<byte[]>(100);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var queuedData = _queuedData.Take();
            Buffer.BlockCopy(queuedData, 0, buffer, offset, Math.Min(queuedData.Length, count));
            return queuedData.Length;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            count = _decoder.DecodeFrame(buffer, offset, count, _buffer, 0);
            var newBuffer = new byte[count];
            Buffer.BlockCopy(_buffer, 0, newBuffer, 0, count);
            _queuedData.Add(newBuffer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                _decoder.Dispose();
        }
    }
}
