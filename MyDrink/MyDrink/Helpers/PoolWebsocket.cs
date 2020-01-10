using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using MyDrink.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MyDrink.Helpers
{
    public class PoolWebsocket
    {
        public event EventHandler<string> DataRecieved;
        public PoolWebsocket()
        {
        }
        public async void StartLoadingData(string id)
        {
            var socket = IO.Socket("https://mydrink-api.herokuapp.com");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("authentication", id);
            });
            socket.On("NEW_ORDER", (data) =>
            {
                if (data != null)
                {
                    DataRecieved?.Invoke(this, data.ToString());
                }
            });
           
            socket.On(Socket.EVENT_DISCONNECT, (data) =>
            {
                Console.WriteLine("DISCONNECT");
            });

        }
        public async void StopLoadingData()
        {
            
        }
        public class Auth 
        {
            public string phoneNumber { get; set; }
            public string password { get; set; }
            public Auth()
            {

            }
        }
        public void Dispose(string phoneNumber)
        {
           
        }
        public class DataSocket
        {
            public int _id { get; set; }
            public string drinkId { get; set; }
            public string drinkName { get; set; }
            public float drinkPrice { get; set; }
            public float totalPrice { get; set; }
            public int drinkQuantity { get; set; }
            public int quantity { get; set; }
            public string detail { get; set; }
            public string imgSrc { get; set; }
            public DataSocket()
            {

            }
            public DataSocket(string dId, string dName, float price, float dPrice, int q, string dt, string src)
            {
                this.drinkId = dId;
                this.drinkName = dName;
                this.drinkPrice = price;
                this.totalPrice = dPrice;
                this.quantity = q;
                this.detail = dt;
                this.imgSrc = src;
            }
        }

    }
}
