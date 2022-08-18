using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using WpfMovies.Models;

namespace WpfMovies.Windows
{
    /// <summary>
    /// Interaction logic for ActorPage.xaml
    /// </summary>
    public partial class ActorPage : Window
    {
        public ActorPage()
        {
            InitializeComponent();
        }

        internal Actor? Actor { get; set; }

        private async void ButtonSaveChanges_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            HttpClient client = new HttpClient();
            if(Actor == null)
            {
                addActor();
            }
            else
            {
                updateActor();
            }
           
        }

        private async void addActor()
        {
            HttpClient client = new HttpClient();
            Actor actor = new Actor();
            actor.Name = textBoxActorName.Text;
            actor.LastName = textBoxActorLastName.Text;
            var jsonActor = JsonConvert.SerializeObject(actor);
            HttpContent content = new StringContent(jsonActor, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync("http://localhost:24818/api/Actor" , content);
            if(message.IsSuccessStatusCode)
            {
                MessageBox.Show("Actor Added Successfully");
            }
            else
            {
                MessageBox.Show("Server Error");
            }
        }

        private async void updateActor()
        {
            HttpClient client = new HttpClient();
            Actor actor = new Actor();
            actor.Name = textBoxActorName.Text;
            actor.LastName = textBoxActorLastName.Text;
            int actorId = Actor.Id;
            var jsonActor = JsonConvert.SerializeObject(actor);
            HttpContent content = new StringContent(jsonActor, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PutAsync("http://localhost:24818/api/Actor/" + actorId, content);
            if(message.IsSuccessStatusCode)
            {
                MessageBox.Show("Actor Details Updated Successfully");
            }
            else
            {
                MessageBox.Show("Server Error - could not update, Status Code:" + message.StatusCode);
            }
        }


        private void ButtonCloseWindow_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
           this.Close();
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            if(Actor != null)
            {
                textBoxActorName.Text = Actor.Name;
                textBoxActorLastName.Text = Actor.LastName;
            }
        }
    }
}
