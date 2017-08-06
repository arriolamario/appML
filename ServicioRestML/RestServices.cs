using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioRestML
{
    public class RestServices
    {
        public IRestResponse Get(string resource)
        {
            IRestResponse response;
            try
            {
                var url = GetConfig("urlML");
                RestClient client = new RestClient(url);
                RestRequest request = new RestRequest(resource, Method.GET);
                var key = GetConfig("keyPublic");
                //request.Parameters.Add(new Parameter() { Name = "public_key", Value = key });
                //request.AddUrlSegment("public_key", key);
                //request.Parameters.AddRange(parametros);
                response = client.Execute(request);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        private String GetConfig(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(name);

        }


        public List<MedioPago> GetMedioPago()
        {
            List<MedioPago> retorno = new List<MedioPago>();
            JsonArray jsonArray;
            if (Convert.ToBoolean(GetConfig("simulado")))
            {
                jsonArray = medioPagoSimulado.Deserealizar<JsonArray>();
            }
            else
            {
                var response = this.Get("/payment_methods?public_key=444a9ef5-8a6b-429f-abdf-587639155d88");
                jsonArray = response.Content.Deserealizar<JsonArray>();
            }

            retorno = cargar<MedioPago>(jsonArray);

            return retorno;

        }

        //
        public List<BancosDTO> GetBancos(string tarjeta)
        {
            List<BancosDTO> retorno = new List<BancosDTO>();
            string consulta = String.Format("/payment_methods/card_issuers?public_key=444a9ef5-8a6b-429f-abdf-587639155d88&payment_method_id={0}", tarjeta);
            JsonArray jsonArray;

            if (Convert.ToBoolean(GetConfig("simulado")))
            {
                jsonArray = simBancos.Deserealizar<JsonArray>();
            }
            else
            {
                var response = Get(consulta);
                jsonArray = response.Content.Deserealizar<JsonArray>();
            }

            retorno = cargar<BancosDTO>(jsonArray);
            return retorno;
        }

        public List<CuotasDTO> GetCantidadCuotas(string banco, double monto, string tarjeta)
        {
            List<CuotasDTO> retorno = new List<CuotasDTO>();
            string consulta = String.Format("/payment_methods/installments?public_key=444a9ef5-8a6b-429f-abdf-587639155d88&amount={0}&payment_method_id={1}&issuer.id={2}",monto, tarjeta, banco);
            JsonArray jsonArray;

            if (Convert.ToBoolean(GetConfig("simulado")))
            {
                jsonArray = new JsonArray();
            }
            else
            {
                var response = Get(consulta);
                jsonArray = response.Content.Deserealizar<JsonArray>();
            }

            retorno = cargar<CuotasDTO>(jsonArray);
            return retorno;
        }

        public List<T> cargar<T>(JsonArray jsonArray)
        {
            List<T> retorno = new List<T>();
            foreach (var item in jsonArray)
            {
                var objecto = item.ToString().Deserealizar<T>();
                retorno.Add(objecto);
            }
            
            return retorno;
        }


        string sim = @"{
    ""id"": ""visa"",
    ""name"": ""Visa"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/visa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/visa.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^4"",
          ""installments_pattern"": ""^4"",
          ""exclusion_pattern"": ""^(400276|400615|402914|404625|405069|405515|405516|405755|405896|405897|406290|406291|406375|406652|406998|406999|408515|410082|410083|410121|410123|410853|411849|417309|421738|423623|428062|428063|428064|434795|437996|439818|442371|442548|444060|446343|446344|446347|450412|450799|451377|451701|451751|451756|451757|451758|451761|451763|451764|451765|451766|451767|451768|451769|451770|451772|451773|457596|457665|462815|463465|468508|473710|473711|473712|473714|473715|473716|473717|473718|473719|473720|473721|473722|473725|477051|477053|481397|481501|481502|481550|483002|483020|483188|489412|492528|499859)""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  }";

        string medioPagoSimulado = @"[
  {
    ""id"": ""visa"",
    ""name"": ""Visa"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/visa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/visa.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^4"",
          ""installments_pattern"": ""^4"",
          ""exclusion_pattern"": ""^(400276|400615|402914|404625|405069|405515|405516|405755|405896|405897|406290|406291|406375|406652|406998|406999|408515|410082|410083|410121|410123|410853|411849|417309|421738|423623|428062|428063|428064|434795|437996|439818|442371|442548|444060|446343|446344|446347|450412|450799|451377|451701|451751|451756|451757|451758|451761|451763|451764|451765|451766|451767|451768|451769|451770|451772|451773|457596|457665|462815|463465|468508|473710|473711|473712|473714|473715|473716|473717|473718|473719|473720|473721|473722|473725|477051|477053|481397|481501|481502|481550|483002|483020|483188|489412|492528|499859)""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""master"",
    ""name"": ""Mastercard"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/master.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/master.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(5|(2(221|222|223|224|225|226|227|228|229|23|24|25|26|27|28|29|3|4|5|6|70|71|720)))"",
          ""installments_pattern"": ""^(?!(554730))"",
          ""exclusion_pattern"": ""^(589657|589562|557039|522135|522137|527555|542702|544764|550073|528824|511849|551238|501105|501020|501021|501023|501062|501038|501057|588729|501041|501056|501075|501080|501081)""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_type"",
      ""cardholder_name"",
      ""cardholder_identification_number"",
      ""issuer_id""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""amex"",
    ""name"": ""American Express"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/amex.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/amex.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 15
        },
        ""bin"": {
          ""pattern"": ""^((34)|(37))"",
          ""installments_pattern"": ""^((34)|(37))"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 4,
          ""card_location"": ""front"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_number"",
      ""cardholder_identification_type"",
      ""cardholder_name""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""naranja"",
    ""name"": ""Naranja"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/naranja.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/naranja.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""none"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(589562)"",
          ""installments_pattern"": ""^(589562)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_type"",
      ""cardholder_name"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""nativa"",
    ""name"": ""Nativa Mastercard"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/nativa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/nativa.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^((520053)|(546553)|(554472)|(531847)|(527601))"",
          ""installments_pattern"": ""^((520053)|(546553)|(554472)|(531847)|(527601))"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_number"",
      ""cardholder_name"",
      ""cardholder_identification_type""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""tarshop"",
    ""name"": ""Tarjeta Shopping"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/tarshop.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/tarshop.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""none"",
          ""length"": 13
        },
        ""bin"": {
          ""pattern"": ""^(27995)"",
          ""installments_pattern"": ""^(27995)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 0,
          ""card_location"": ""back"",
          ""mode"": ""optional""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_number"",
      ""cardholder_identification_type""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""cencosud"",
    ""name"": ""Cencosud"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/cencosud.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/cencosud.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(603493)"",
          ""installments_pattern"": ""^(603493)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""cabal"",
    ""name"": ""Cabal"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/cabal.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/cabal.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^((627170)|(589657)|(603522)|(604((20[1-9])|(2[1-9][0-9])|(3[0-9]{2})|(400))))"",
          ""installments_pattern"": ""^((627170)|(589657)|(603522)|(604((20[1-9])|(2[1-9][0-9])|(3[0-9]{2})|(400))))"",
          ""exclusion_pattern"": ""^(604201)""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""diners"",
    ""name"": ""Diners"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/diners.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/diners.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 14
        },
        ""bin"": {
          ""pattern"": ""^((30)|(36)|(38))"",
          ""installments_pattern"": ""^((360935)|(360936))"",
          ""exclusion_pattern"": ""^((3646)|(3648))""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_type"",
      ""cardholder_name"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""pagofacil"",
    ""name"": ""Pago Fácil"",
    ""payment_type_id"": ""ticket"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/pagofacil.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/pagofacil.gif"",
    ""deferred_capture"": ""does_not_apply"",
    ""settings"": [
    ],
    ""additional_info_needed"": [
    ],
    ""min_allowed_amount"": 10,
    ""max_allowed_amount"": 15000,
    ""accreditation_time"": 0,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""argencard"",
    ""name"": ""Argencard"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/argencard.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/argencard.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(501105)"",
          ""installments_pattern"": ""^(501105)"",
          ""exclusion_pattern"": ""^((589562)|(527571)|(527572))""
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_type"",
      ""cardholder_name"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""maestro"",
    ""name"": ""Maestro"",
    ""payment_type_id"": ""debit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/maestro.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/maestro.gif"",
    ""deferred_capture"": ""unsupported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""none"",
          ""length"": 18
        },
        ""bin"": {
          ""pattern"": ""^(501020|501021|501023|501062|501038|501057|588729|501041|501056|501075)"",
          ""installments_pattern"": null,
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      },
      {
        ""card_number"": {
          ""validation"": ""none"",
          ""length"": 19
        },
        ""bin"": {
          ""pattern"": ""^(501080|501081)"",
          ""installments_pattern"": null,
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_number"",
      ""cardholder_identification_type"",
      ""cardholder_name""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 1440,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""debmaster"",
    ""name"": ""Mastercard Débito"",
    ""payment_type_id"": ""debit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/debmaster.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/debmaster.gif"",
    ""deferred_capture"": ""unsupported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(546367|557648|511849|517562|528745|549180|528733|551792|553771|553777|553839)"",
          ""installments_pattern"": ""^(546367|557648|511849|517562|528745|549180|528733|551792|553771|553777|553839)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_number"",
      ""cardholder_identification_type"",
      ""cardholder_name"",
      ""issuer_id""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 1440,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""debcabal"",
    ""name"": ""Cabal Débito"",
    ""payment_type_id"": ""debit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/debcabal.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/debcabal.gif"",
    ""deferred_capture"": ""unsupported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(604201)"",
          ""installments_pattern"": ""^(604201)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_identification_number"",
      ""cardholder_identification_type"",
      ""cardholder_name""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 10000,
    ""accreditation_time"": 1440,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""debvisa"",
    ""name"": ""Visa Débito"",
    ""payment_type_id"": ""debit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/debvisa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/debvisa.gif"",
    ""deferred_capture"": ""unsupported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(400276|405069|405755|400615|405896|402789|405897|402914|406290|404625|406291|405515|406998|405516|406999|405517|410082|406375|410083|406652|439818|408515|444060|410121|450412|410122|451377|410123|463465|410853|473711|411849|473725|417309|477051|421738|483020|423623|428062|428063|428064|434795|437996|442371|442548|444493|446343|446347|451701|451751|451756|451757|451758|451761|451763|451764|451765|451766|451767|451768|451769|451770|451772|451773|457596|457665|462815|468508|473227|473710|473712|473713|473714|473715|473716|473717|473718|473719|473720|473721|473722|476520|477053|481397|481501|481502|481550|483002|483188|492528|450799|489412|499859)"",
          ""installments_pattern"": ""^(400276|405069|405755|400615|405896|402789|405897|402914|406290|404625|406291|405515|406998|405516|406999|405517|410082|406375|410083|406652|439818|408515|444060|410121|450412|410122|451377|410123|463465|410853|473711|411849|473725|417309|477051|421738|483020|423623|428062|428063|428064|434795|437996|442371|442548|444493|446343|446347|451701|451751|451756|451757|451758|451761|451763|451764|451765|451766|451767|451768|451769|451770|451772|451773|457596|457665|462815|468508|473227|473710|473712|473713|473714|473715|473716|473717|473718|473719|473720|473721|473722|476520|477053|481397|481501|481502|481550|483002|483188|492528|450799|489412|499859)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""rapipago"",
    ""name"": ""Rapipago"",
    ""payment_type_id"": ""ticket"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/rapipago.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/rapipago.gif"",
    ""deferred_capture"": ""does_not_apply"",
    ""settings"": [
    ],
    ""additional_info_needed"": [
    ],
    ""min_allowed_amount"": 0.01,
    ""max_allowed_amount"": 60000,
    ""accreditation_time"": 0,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""cargavirtual"",
    ""name"": ""Kioscos y comercios cercanos"",
    ""payment_type_id"": ""ticket"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/cargavirtual.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/cargavirtual.gif"",
    ""deferred_capture"": ""does_not_apply"",
    ""settings"": [
    ],
    ""additional_info_needed"": [
    ],
    ""min_allowed_amount"": 0.01,
    ""max_allowed_amount"": 5000,
    ""accreditation_time"": 0,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""cordobesa"",
    ""name"": ""Cordobesa"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/cordobesa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/cordobesa.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^((542702)|(544764)|(550073)|(528824))"",
          ""installments_pattern"": ""^((542702)|(544764)|(550073))"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  },
  {
    ""id"": ""cmr"",
    ""name"": ""CMR"",
    ""payment_type_id"": ""credit_card"",
    ""status"": ""active"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/cmr.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/cmr.gif"",
    ""deferred_capture"": ""supported"",
    ""settings"": [
      {
        ""card_number"": {
          ""validation"": ""standard"",
          ""length"": 16
        },
        ""bin"": {
          ""pattern"": ""^(557039)"",
          ""installments_pattern"": ""^(557039)"",
          ""exclusion_pattern"": null
        },
        ""security_code"": {
          ""length"": 3,
          ""card_location"": ""back"",
          ""mode"": ""mandatory""
        }
      }
    ],
    ""additional_info_needed"": [
      ""cardholder_name"",
      ""cardholder_identification_type"",
      ""cardholder_identification_number""
    ],
    ""min_allowed_amount"": 0,
    ""max_allowed_amount"": 250000,
    ""accreditation_time"": 2880,
    ""financial_institutions"": [
    ],
    ""processing_modes"": [
      ""aggregator""
    ]
  }
]";


        String simBancos = @"[
  {
    ""id"": ""288"",
    ""name"": ""Tarjeta Shopping"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/288.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/288.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""319"",
    ""name"": ""Citi"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/319.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/319.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""279"",
    ""name"": ""Banco Galicia"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/279.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/279.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""287"",
    ""name"": ""Banco Santa Cruz"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/287.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/287.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""333"",
    ""name"": ""Nuevo Banco de Santa Fe"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/333.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/333.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""297"",
    ""name"": ""Macro"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/297.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/297.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""310"",
    ""name"": ""Banco Santander Rio S.A."",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/310.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/310.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""294"",
    ""name"": ""Banco Hipotecario"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/294.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/294.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1005"",
    ""name"": ""Provencred"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1005.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1005.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""282"",
    ""name"": ""Banco Nacion"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/282.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/282.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""338"",
    ""name"": ""ICBC"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/338.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/338.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""286"",
    ""name"": ""Banco San Juan"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/286.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/286.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""326"",
    ""name"": ""HSBC"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/326.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/326.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1026"",
    ""name"": ""Banco Industrial"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1026.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1026.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1016"",
    ""name"": ""Banco Ciudad"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1016.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1016.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""296"",
    ""name"": ""Itau"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/296.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/296.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""272"",
    ""name"": ""Banco Comafi"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/272.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/272.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""316"",
    ""name"": ""BBVA Frances"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/316.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/316.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""303"",
    ""name"": ""Banco Patagonia"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/303.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/303.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""331"",
    ""name"": ""Nuevo Banco de Entre Rios"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/331.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/331.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""313"",
    ""name"": ""Banco Supervielle"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/313.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/313.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1043"",
    ""name"": ""Tucuman"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1043.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1043.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1044"",
    ""name"": ""Banco Provincia"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1044.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1044.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""1049"",
    ""name"": ""Banco de La Pampa"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/1049.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/1049.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""2035"",
    ""name"": ""Banco de Chaco"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/2035.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/2035.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""2033"",
    ""name"": ""Banco Bica"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/2033.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/2033.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  },
  {
    ""id"": ""2040"",
    ""name"": ""Fueguina"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/2040.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/2040.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": ""1""
  },
  {
    ""id"": ""10000"",
    ""name"": ""Banco de Chubut"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/10000.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/10000.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": ""1""
  },
  {
    ""id"": ""10002"",
    ""name"": ""Banco Columbia"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/10002.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/10002.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": ""1""
  },
  {
    ""id"": ""10004"",
    ""name"": ""Banco Municipal"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/10004.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/10004.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": ""1""
  },
  {
    ""id"": ""10005"",
    ""name"": ""Banco Saenz"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/10005.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/10005.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": ""1""
  },
  {
    ""id"": ""1"",
    ""name"": ""Otro"",
    ""secure_thumbnail"": ""https://www.mercadopago.com/org-img/MP3/API/logos/visa.gif"",
    ""thumbnail"": ""http://img.mlstatic.com/org-img/MP3/API/logos/visa.gif"",
    ""processing_mode"": ""aggregator"",
    ""merchant_account_id"": null
  }
]";
    }
}
