using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mvc.Repository.Models
{
    [Table("Funcionario")]
    [Index(nameof(FuncionarioNome), nameof(FuncionarioSalario), Name = "nomeesalarioIsUNIQUE", IsUnique = true)]
    public partial class Funcionario
    {
        public Funcionario()
        {
            FilhoFilhoFuncionarioMaeNavigations = new HashSet<Filho>();
            FilhoFilhoFuncionarioPaiNavigations = new HashSet<Filho>();
        }

        [Key]
        [Column("Funcionario_id")]
        public int FuncionarioId { get; set; }
        [Column("Funcionario_nome")]
        [StringLength(50)]
        [Unicode(false)]
        public string? FuncionarioNome { get; set; }
        [Column("Funcionario_datadenascimento", TypeName = "datetime")]
        public DateTime? FuncionarioDatadenascimento { get; set; }
        [Column("Funcionario_salario", TypeName = "decimal(18, 0)")] 
        [Required(ErrorMessage = "Salario e obrigatorio")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Salario tem que ser maior que 0")]
        public decimal? FuncionarioSalario { get; set; }

        [InverseProperty(nameof(Filho.FilhoFuncionarioMaeNavigation))]
        public virtual ICollection<Filho> FilhoFilhoFuncionarioMaeNavigations { get; set; }
        [InverseProperty(nameof(Filho.FilhoFuncionarioPaiNavigation))]
        public virtual ICollection<Filho> FilhoFilhoFuncionarioPaiNavigations { get; set; }
    }
}
