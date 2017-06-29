using System;
using System.Linq;
using System.Collections.Generic;
using NeuroSpeech.UIAtoms;
using System.Text;
using UIAtomsDemo.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;

namespace UIAtomsDemo
{

    public class Customer {

        [TextField(
            Category = "Agency Requirements", 
            Label = "First Name", 
            Placeholder = "First",
            Description = "Stage Name")]
        public string ARStageFirstName { get; set; }

        [TextField(
            Category = "Agency Requirements",
            Label = "Last Name",
            Placeholder = "Last",
            Description = "Stage Name")]
        public string ARStageLastName { get; set; }

        [TextField(
            Category = "Agency Requirements",
            Label = "First Name",
            Placeholder = "First",
            Description = "Legal Name")]
        public string ARLegalFirstName { get; set; }

        [TextField(
            Category = "Agency Requirements",
            Label = "Last Name",
            Placeholder = "Last",
            Description = "Legal Name")]
        public string ARLegalLastName { get; set; }

        [BirthDateField(
            Category = "Agency Requirements",
            Label = "Birth Date",
            Description = "Birth date is required for legal compliance in adult commercials"
            )]
        public DateTime BirthDate { get; set; }


        [TextField(
            Category = "Personal",
            Label = "Passport Number",
            Description = "Your current passport number"
            )]
        public string PassportNumber { get; set; }

        [TextField(
            Category = "Personal",
            Label = "Citizen",
            Description = "Your current primary citizenship"
            )]
        public string Citizen { get; set; }

        [SwitchField(
            Category = "Emails",
            Label = "Receive Breakdowns"
            )]
        public bool ReceiveBreakdowns { get; set; }

        [SwitchField(
            Category = "Emails",
            Label = "Receive Workshops"
            )]
        public bool ReceiveWorkshops { get; set; }

    }

}