using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppointmentScheduler.Domain.Enums;
using AppointmentScheduler.Domain.ValueObjects;

namespace AppointmentScheduler.Domain.Entities
{
    [Table("Consultas")]
    public class Consulta
    {
        [Key]
        public int Id { get; private set; }
        [Required(ErrorMessage = "O nome do paciente é um campo obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome do paciente não pode exceder 100 caracteres")]
        public string Paciente { get; private set; } = null!;
        public string? Descricao { get; private set; }
        [Required(ErrorMessage = "A data da consulta é um campo obrigatório")]
        public DataDaConsulta DataDaConsulta { get; private set; } = null!;
        [Required(ErrorMessage = "O status da consulta é um campo obrigatório")]
        public StatusEnum Status { get; private set; } = StatusEnum.Agendado;
        public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

        public Consulta(string paciente, DataDaConsulta dataDaConsulta, StatusEnum status, string? descricao = null)
        {
            Paciente = paciente;
            DataDaConsulta = dataDaConsulta;
            Status = status;
            Descricao = descricao;
        }
    }
}
