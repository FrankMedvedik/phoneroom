namespace PhoneLogic.Model
{
    using System.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class person
    {
        private string _last_name;
        [Display(Name = "Last")]
        public string last_name
        {
            get { return _last_name; }
            set
            {
                _last_name = value;

            }
        }

        private string _first_name;
        [Display(Name = "First")]
        public string first_name
        {
            get { return _first_name; }
            set
            {
                _first_name = value;
            }
        }

        private string _middle_name;
        [Display(Name = "Middle")]
        public string middle_name
        {
            get { return _middle_name; }
            set
            {
                _middle_name = value;
            }
        }
        
        
        
        

        private string _name_prefix;
        [Display(Name = "Name Prefix")]
        public string name_prefix
        {
            get { return _name_prefix; }
            set
            {
                _name_prefix = value;
            }
        }

        
        [Display(Name = "Suffix")]
        private string _name_suffix;
        public string name_suffix
        {
            get { return _name_suffix; }
            set
            {
                _name_suffix = value;
            }
        }


        private string _title;
        [Display(Name = "Title")]
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
            }
        }

        private DateTime? _birth_date;
        [Display(Name = "DOB")]
        public DateTime? birth_date 
        {
            get { return _birth_date; }
            set
            {
                _birth_date = value;
            }
        }

        private string _gender;
        [Display(Name = "Gender")]
        public string gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }

        private string _is_active;
        [Display(Name = "Active")]
        private string is_active
        {
            get { return _is_active; }
            set
            {
                _is_active = value;
            }
        }

        
        private int? _contact_pref;
        [Display(Name = "Contact Pref")]
        public int? contact_pref
        {
            get { return _contact_pref; }
            set
            {
                _contact_pref = value;
            }
        }

        
        [Display(Name = "Updated")]
        private DateTime _updated; 
        private DateTime updated
        {
            get { return _updated; }
            set
            {
                _updated = value;
            }
        }
        [Display(Name = "Created")]
        private DateTime  _created; 
        public DateTime created
        {
            get { return _created; }
            set
            {
                _created = value;
            }
        }

        [Display(Name = "Updated By")]
        private string _updated_user ;
        public string updated_user 
        {
            get { return _updated_user ; }
            set
            {
                _updated_user  = value;
            }
        }
        [Display(Name = "Created By")]
        private string _created_user;
        public string created_user 
        {
            get { return _created_user ; }
            set
            {
                _created_user  = value;
            }
        }
        private string _person_id;

        [Display(Name = "Person Id")]
        public string person_id
        {
            get { return _person_id; }
            set
            {
                _person_id = value;
            }
        }

//        public virtual ICollection<Phone> phone { get; set; }
    }
}

