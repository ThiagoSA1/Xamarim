using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTÃO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
           string cep = CEP.Text.Trim();

            if (ValidarCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {0},{1} {2}", end.localidade, end.uf, end.logradouro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP " + CEP.text);
                    }

                   
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
               // throw;
                }
             
                
            }
            else
            {

            }

          
        }

        private bool ValidarCEP(string cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve contem 8 caracteres.", "OK");
                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
