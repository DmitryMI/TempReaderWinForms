namespace TempReaderWinForms.DataSmoothing
{
    interface IDataSmoother
    {
        float GetLastValue();
        float GetValue(int backIndex);
        int MaxBackIndex { get; }

        void AddValue(float value);
    }
}
