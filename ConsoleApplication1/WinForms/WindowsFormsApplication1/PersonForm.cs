using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EstudosCSharp.Delegates;

namespace WindowsFormsApplication1
{
    public partial class PersonForm : Form
    {
        public PersonForm()
        {
            InitializeComponent();
        }

        private Person _person;

        /// <summary>
        /// PropertyGrid control has a SelectedObject property that can be set to the object it needs
        ///     to visualize. Here we’re binding it to the _person instance and specifying null for the
        ///     dataMember argument. This means we want to visualize the whole object and not just a
        ///     specific property of it.
        ///     
        /// The next two lines establish bindings to the TextBox controls, which are named after the
        /// data members they display. This time the binding targets the Text property of the control,
        /// with the source being the Name and Age properties on _person.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonForm_Load(object sender, EventArgs e)
        {
            _person = new Person("Helder", 34);
            propertyGrid1.DataBindings.Add("SelectedObject", _person, null); 
            txtName.DataBindings.Add("Text", _person, "Name");
            txtAge.DataBindings.Add("Text", _person, "Age");
            
            /*
             Declaring the data bindings suffices to have the framework set up all plumbing on your
behalf. Data binding sources, as specified in the second parameter to the Add method, that
implement INotifyPropertyChanged will cause the framework to register an event
handler for the PropertyChanged event. The data binding logic reacts to this event by
checking whether the changed property (matched by its name) should cause a bound
control to update.
              */

            /*  The one way described earlier covers changes to the underlying object getting mirrored in the
user interface controls.*/
              
/*             a change to the Name text box triggers a cascade of events:
>> A data binding for the Text property on the text box is located and found.
>> The target of the binding, the Name property on _person, is updated.
>> Our Person implementation triggers a PropertyChanged event for Name.
>> Data binding logic has subscribed to the event and looks for update targets.
>> Because _person changed, SelectedObject on propertyGrid gets updated.*/

            /*
             The scenario illustrated here can easily be tested: Just enter a new name or age in any of
the two TextBox controls and make the control lose focus (for example, using Tab). At that
point, the data binding logic kicks in and the PropertyGrid control will reflect the update
made. Notice all of this also works in the other direction: An update to one of the properties
displayed in the PropertyGrid will make its way to the underlying object, causing a
PropertyChanged event that will be picked up by the data binding layer. And that, in turn,
will cause the affected TextBox control to update, too. Sweet!
             */
        }


    }
}
