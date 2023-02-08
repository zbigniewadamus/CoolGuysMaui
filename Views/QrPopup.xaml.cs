namespace CoolGuys.Views;

public partial class QrPopup
{
	public QrPopup(string data)
	{
		InitializeComponent();
		qrImage.Source = $"https://api.qrserver.com/v1/create-qr-code/?size=200x200&data={data}";
	}
}