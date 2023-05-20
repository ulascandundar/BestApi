using Business.Abstract;
using QRCoder;

namespace Business.Concrete
{
	public class QrCodeService:IQrCodeService
	{
		public byte[] GenerateQrCode(string text)
		{
			QRCodeGenerator generator = new QRCodeGenerator();
			QRCodeData data = generator.CreateQrCode(text,QRCodeGenerator.ECCLevel.Q);
			PngByteQRCode qRCode = new PngByteQRCode(data);
			byte[] byteGraphic = qRCode.GetGraphic(5, new byte[] { 84, 99, 81 }, new byte[] { 240, 240, 240 });
			return byteGraphic;
		}
	}
}
