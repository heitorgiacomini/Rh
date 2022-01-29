using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mvc.Repository.Models
{
    [Table("Filho")]
    public partial class Filho
    {
        [Key]
        [Column("Filho_id")]
        public int FilhoId { get; set; }
        [Column("Filho_nome")]
        [StringLength(50)]
        [Unicode(false)]
        public string? FilhoNome { get; set; }
        [Column("Filho_datadenascimento", TypeName = "datetime")]
        public DateTime? FilhoDatadenascimento { get; set; }
        [Column("Filho_FuncionarioPai")]
        public int? FilhoFuncionarioPai { get; set; }
        [Column("Filho_FuncionarioMae")]
        public int? FilhoFuncionarioMae { get; set; }

        [ForeignKey(nameof(FilhoFuncionarioMae))]
        [InverseProperty(nameof(Funcionario.FilhoFilhoFuncionarioMaeNavigations))]
        public virtual Funcionario? FilhoFuncionarioMaeNavigation { get; set; }
        [ForeignKey(nameof(FilhoFuncionarioPai))]
        [InverseProperty(nameof(Funcionario.FilhoFilhoFuncionarioPaiNavigations))]
        public virtual Funcionario? FilhoFuncionarioPaiNavigation { get; set; }
    }
}
