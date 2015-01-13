namespace PhoneLogic.Model
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LyncStatusRange
    {
        public string Name{ get; set; }
        public int MinValue{ get; set; }
        public int MaxValue{ get; set; }
    };
    
    
}