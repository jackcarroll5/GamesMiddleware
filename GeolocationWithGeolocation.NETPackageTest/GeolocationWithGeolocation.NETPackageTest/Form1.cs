using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeolocationWithGeolocation.NETPackageTest
{
    //Reference - Some of the actions based on tutorial followed for getting location within the Windows Form with the .NET Framework using the GeoCoordinateWatcher feature - http://irisclasson.com/2015/09/21/geolocation-net-4-and-up-requested-winforms-example/
    public partial class Form1 : Form
    {
        private GeoCoordinateWatcher _geolocationwatcher;

        public Form1()
        {
            InitializeComponent();

            _geolocationwatcher = new GeoCoordinateWatcher();

            _geolocationwatcher.StatusChanged += GeoLocationChangedStatus;

        }

        private void GeoLocationChangedStatus(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status != GeoPositionStatus.Ready)
                return;

            GeoPosition<GeoCoordinate> coordinates = _geolocationwatcher.Position;

            GeoCoordinate devPosition = coordinates.Location;

            DateTimeOffset retrievedAt = coordinates.Timestamp;

            GeoPositionPermission allow = _geolocationwatcher.Permission;

            lblLocationPosition.Text = string.Format("Latitude: {0}, Longitude: {1}, retrieved at: {2},\nVertical Accuracy : {3},\n Speed : {4},\n Altitude : {5},\n Course : {6},\n Is Location Unknown : {7},\n Horizontal Accuracy : {8}", devPosition.Latitude, devPosition.Longitude, retrievedAt.DateTime.ToLongTimeString(),devPosition.VerticalAccuracy,devPosition.Speed,devPosition.Altitude, devPosition.Course, devPosition.IsUnknown.ToString(),devPosition.HorizontalAccuracy);
        }

        private void btnLocationRetrieval_Click(object sender, EventArgs e)
        {
            _geolocationwatcher.Start();

            btnLocationRetrieval.Enabled = false;
        }

       
    }
}
