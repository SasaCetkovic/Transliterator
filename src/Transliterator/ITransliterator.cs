namespace Transliterator
{
	public interface ITransliterator
	{
		string ToLatin(string text);
		string ToCyrillic(string text);
	}
}
