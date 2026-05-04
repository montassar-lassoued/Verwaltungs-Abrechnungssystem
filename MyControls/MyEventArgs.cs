namespace MyControls
{
    public class MyEventArgs
    {
        public MyEventArgs(object value)
        {
            Value = value;
        }
        public object Value
        { get; }
    }
}
