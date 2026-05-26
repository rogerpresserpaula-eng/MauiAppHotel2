using MauiAppHotel2.Models;

namespace MauiAppHotel2.Views;

public partial class ContratacaoHospedagem : ContentPage
{
	App PropriedadesApp;

	public ContratacaoHospedagem()
	{
		InitializeComponent();

		PropriedadesApp = (App)Application.Current;

		pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

		dtpck_checkin.MinimumDate = DateTime.Now;
		dtpck_checkin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);

        dtpck_checkout.MinimumDate = dtpck_checkin.Date.Value.AddDays(1);
        dtpck_checkout.MaximumDate = dtpck_checkin.Date.Value.AddMonths(6);
    }

    private async void Button_Clicked(object sender, EventArgs e) //Determina a página inicial
    {
		try
		{
			Hospedagem h = new Hospedagem
			{
				QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
				QntAdultos = Convert.ToInt32(stp_adultos.Value),
				QntCriancas = Convert.ToInt32(stp_criancas.Value),
				DataCheckin = dtpck_checkin.Date.Value,
				DataCheckout = dtpck_checkout.Date.Value,

            };


			await Navigation.PushAsync(new HospedagemContratada()
			{
				BindingContext = h
		
			});
				
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
		DatePicker elemento = sender as DatePicker;

		DateTime data_selecionada_chekin = elemento.Date.Value;

		dtpck_checkout.MinimumDate = data_selecionada_chekin.AddDays(1);
		dtpck_checkout.MaximumDate = data_selecionada_chekin.AddMonths(6);
    }
}