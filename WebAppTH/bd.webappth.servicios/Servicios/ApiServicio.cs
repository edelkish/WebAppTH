﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using bd.webappth.servicios.Interfaces;
using bd.webappth.entidades.Utils;

namespace bd.webappth.servicios.Servicios
{
    public class ApiServicio : IApiServicio
    {
        public async Task<Response> InsertarAsync<T>(T model, Uri baseAddress, string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(model);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    client.BaseAddress = baseAddress;

                    var response = await client.PostAsync(url, content);

                    var resultado = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Response>(resultado);
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> EliminarAsync(string id, Uri baseAddress, string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = baseAddress;
                    url = string.Format("{0}/{1}", url, id);

                    var response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Error",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Insertar Ok",

                    };

                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> EditarAsync<T>(string id,T model, Uri baseAddress, string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = JsonConvert.SerializeObject(model);
                    var content = new StringContent(request, Encoding.UTF8, "application/json");

                    client.BaseAddress = baseAddress;
                    url = string.Format("{0}/{1}", url, id);

                    var response = await client.PutAsync(url,content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Error",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Insertar Ok",

                    };

                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = ex.Message,
                };
            }
        }
        public async Task<List<T>> Listar<T>(Uri baseAddress, string url) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = baseAddress;
                    var respuesta = await client.GetAsync(url);

                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<List<T>>(resultado);
                    return response;
                }
            }

            catch (Exception )
            {
                return null;
            }
                           
        }
        public async Task<T> SeleccionarAsync<T>(string id,Uri baseAddress, string url) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = baseAddress;
                    url = string.Format("{0}/{1}", url, id);
                    var respuesta = await client.GetAsync(url);

                    var resultado = await respuesta.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<T>(resultado);
                    return response;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}