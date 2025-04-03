using System.ComponentModel;
using System.Text;

namespace rtttl_composer_library;

public static class PcmAudio
{
    public static MemoryStream PackWaveBuffer(IEnumerable<SoundToken> sounds)
    {
        return PackWaveBuffer(sounds.SelectMany(CreateSample).ToArray());
    }
    
    static MemoryStream PackWaveBuffer(Int16[] samples)
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream, Encoding.ASCII);
        var dataLengthBytes = samples.Length * 2;
        
        writer.Write(Encoding.ASCII.GetBytes("RIFF"));
        writer.Write(dataLengthBytes + 36);
        writer.Write(Encoding.ASCII.GetBytes("WAVE"));
        writer.Write(Encoding.ASCII.GetBytes("fmt "));
        writer.Write(16);
        writer.Write((Int16) 1);        // PCM
        writer.Write((Int16) 1);        // mono
        writer.Write(44100);            // sample rate
        writer.Write(44100 * 2);        // byte rate (bytes/s)
        writer.Write((Int16) 2);        // bytes per sample
        writer.Write((Int16) 16);       // bits per sample

        // data
        writer.Write(Encoding.ASCII.GetBytes("data"));
        writer.Write(dataLengthBytes);
        byte[] data = new byte[dataLengthBytes];
        Buffer.BlockCopy(samples, 0, data, 0, data.Length);
        writer.Write(data);
        return stream;
    }
    
    static Int16[] CreateSample(SoundToken sound)
    {
        const double volume = 0.8;
        const int sixteenBitSampleLimit = 32767;
        const int sampleRate = 44100;

        return Enumerable.Range(1, Convert.ToInt32(sound.DuractionSeconds * sampleRate))
            .Select(ToAmplitude)
            .ToArray();

        Int16 ToAmplitude(int time)
        {
            return Convert.ToInt16(Math.Sin(
                time * 2 * Math.PI * sound.FrequencyHz / sampleRate) 
                * sixteenBitSampleLimit * volume);
        }
    }
}