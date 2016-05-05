using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class RecordProxy : Proxy, IProxy 
{
	//构造器
	public RecordProxy(){
		Debug.Log ("进入RecordDO的构造器");
		GameObject rec = new GameObject ();
		rec.name = "Audio Record Proxy";
		recordedAudio = rec.AddComponent<AudioSource>();
	}

	//全局录音变量
	private AudioSource recordedAudio;
	private AudioClip recorder;
	private List<float> temp_stitch = new List<float>();
	private int readPos = 0;

	//开始录音
	public void StartRecord()
	{
		Debug.Log ("进入Record Porxy() 开始录音");
		try
		{
			recorder = Microphone.Start(null, false, 300, 12800);
		}
		catch (SystemException e)
		{
			Debug.Log("录音出错了"+e.Message);
		}
	}

	//停止录音
	public void StopRecord()
	{
		Debug.Log ("进入Record Porxy() 停止录音");
		if (Microphone.IsRecording(null))
		{
			temp_stitch.Clear();
			readPos = Microphone.GetPosition(null);
			Microphone.End(null);

			float[] samples = new float[recorder.samples];
			recorder.GetData(samples, 0);

			float[] samples2 = new float[readPos];

			for (int j = 0; j < readPos; j++)
				samples2[j] = samples[j];

			temp_stitch.AddRange(samples2);

			if (temp_stitch.Count > 0)
			{

				AudioClip stitch_clip = AudioClip.Create("clip", temp_stitch.Count, 1, 12800, false, false);
				stitch_clip.SetData(temp_stitch.ToArray(), 0);

				recordedAudio.clip = stitch_clip;

				recordedAudio.Play();

				//StartCoroutine(SaveAudioClip());

			}
		}
	}

	//正常速率播放录音
	public void PlayRecordNomal(){
		Debug.Log ("进入Record Porxy() 正常速率播放录音");
		recordedAudio.pitch = 1f;
		recordedAudio.Play();
	}

	//高音速率播放录音
	public void PlayRecordHight(){
		Debug.Log ("进入Record Porxy() 高音速率播放录音");
		recordedAudio.pitch = 1.5f;
		recordedAudio.Play();
	}

	//定义头文件
	const int HEADER_SIZE = 44;

	//保存录音
	public static bool Save(string filename, AudioClip clip) {
		if (!filename.ToLower().EndsWith(".wav")) {
			filename += ".wav";
		}
		var filepath = Path.Combine(Application.persistentDataPath, filename);
		Debug.Log(filepath);
		// Make sure directory exists if user is saving to sub dir.
		Directory.CreateDirectory(Path.GetDirectoryName(filepath));
		using (var fileStream = CreateEmpty(filepath)) {
			ConvertAndWrite(fileStream, clip);
			WriteHeader(fileStream, clip);
		}
		return true; 
	}

	//转换录音片段
	public static AudioClip TrimSilence(AudioClip clip, float min) {
		var samples = new float[clip.samples];
		clip.GetData(samples, 0);
		return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);
	}
	//转换录音片段
	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz) {
		return TrimSilence(samples, min, channels, hz, false, false);
	}
	//转换录音片段
	public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream) {
		int i;
		for (i=0; i<samples.Count; i++) {
			if (Mathf.Abs(samples[i]) > min) {
				break;
			}
		}
		samples.RemoveRange(0, i);
		for (i=samples.Count - 1; i>0; i--) {
			if (Mathf.Abs(samples[i]) > min) {
				break;
			}
		}
		samples.RemoveRange(i, samples.Count - i);
		var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);
		clip.SetData(samples.ToArray(), 0);
		return clip;
	}

	//新建空白录音
	static FileStream CreateEmpty(string filepath) {
		var fileStream = new FileStream(filepath, FileMode.Create);
		byte emptyByte = new byte();
		for(int i = 0; i < HEADER_SIZE; i++) //preparing the header
		{
			fileStream.WriteByte(emptyByte);
		}
		return fileStream;
	}

	//转换和写入数据
	static void ConvertAndWrite(FileStream fileStream, AudioClip clip) {
		var samples = new float[clip.samples];
		clip.GetData(samples, 0);
		Int16[] intData = new Int16[samples.Length];
		//converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]
		Byte[] bytesData = new Byte[samples.Length * 2];
		//bytesData array is twice the size of
		//dataSource array because a float converted in Int16 is 2 bytes.
		int rescaleFactor = 32767; //to convert float to Int16
		for (int i = 0; i<samples.Length; i++) {
			intData[i] = (short) (samples[i] * rescaleFactor);
			Byte[] byteArr = new Byte[2];
			byteArr = BitConverter.GetBytes(intData[i]);
			byteArr.CopyTo(bytesData, i * 2);
		}
		fileStream.Write(bytesData, 0, bytesData.Length);
	}

	//写入Wav的头部
	static void WriteHeader(FileStream fileStream, AudioClip clip) {
		var hz = clip.frequency;
		var channels = clip.channels;
		var samples = clip.samples;

		fileStream.Seek(0, SeekOrigin.Begin);

		Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(riff, 0, 4);

		Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
		fileStream.Write(chunkSize, 0, 4);

		Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(wave, 0, 4);

		Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(fmt, 0, 4);

		Byte[] subChunk1 = BitConverter.GetBytes(16);
		fileStream.Write(subChunk1, 0, 4);

		UInt16 two = 2;
		UInt16 one = 1;

		Byte[] audioFormat = BitConverter.GetBytes(one);
		fileStream.Write(audioFormat, 0, 2);

		Byte[] numChannels = BitConverter.GetBytes(channels);
		fileStream.Write(numChannels, 0, 2);

		Byte[] sampleRate = BitConverter.GetBytes(hz);
		fileStream.Write(sampleRate, 0, 4);

		Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
		fileStream.Write(byteRate, 0, 4);

		UInt16 blockAlign = (ushort) (channels * 2);
		fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

		UInt16 bps = 16;
		Byte[] bitsPerSample = BitConverter.GetBytes(bps);
		fileStream.Write(bitsPerSample, 0, 2);

		Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
		fileStream.Write(datastring, 0, 4);

		Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
		fileStream.Write(subChunk2, 0, 4);
	}

}
