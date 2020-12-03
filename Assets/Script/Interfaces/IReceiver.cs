public interface IReceiver
{
    // Честно это хак, а не решение
    // Данная ситуация происходит по причине того, что csharp не имеет мульти-наследования классов (только интерфейсов)
    void AddSenderState(int senderId, bool state);
    bool GetSenderState(int senderId);
    /// <summary>
    /// Получение "сигнала"
    /// </summary>
    void Receive(int senderId, bool state);
}