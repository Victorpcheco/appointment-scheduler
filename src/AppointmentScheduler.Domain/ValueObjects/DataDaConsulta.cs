namespace AppointmentScheduler.Domain.ValueObjects
{
    // Declara a classe como sealed (não pode ser herdada), pois Value Objects normalmente não precisam de herança
    public sealed class DataDaConsulta
    {
        public DateTime Value { get; }

        private DataDaConsulta(DateTime value)
        {
            Value = value;
        }

        public static DataDaConsulta Create(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("A data da consulta é obrigatória.");

            if (date < DateTime.UtcNow) 
                throw new ArgumentException("A data da consulta não pode estar no passado.");

            return new DataDaConsulta(date); 
        }

        public override bool Equals(object obj) // Compara Value Objects pelo valor interno
        {
            if (obj is not DataDaConsulta other)
                return false;

            return Value == other.Value;
        }

        public override int GetHashCode() => Value.GetHashCode(); // Mantém consistência com Equals

        public override string ToString() => Value.ToString("yyyy-MM-dd HH:mm"); // Exibe a data formatada
    }
}

