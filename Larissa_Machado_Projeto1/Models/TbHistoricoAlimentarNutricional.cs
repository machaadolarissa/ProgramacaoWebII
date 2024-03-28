﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Larissa_Machado_Projeto1.Models;

[Table("tbHistoricoAlimentarNutricional")]
[Index("IdPaciente", Name = "IX_tbHistoricoAlimentarNutricional_IdPaciente")]
public partial class TbHistoricoAlimentarNutricional
{
    [Key]
    public int IdHistAlimentarNutricional { get; set; }

    public int IdPaciente { get; set; }

    public int? MotivacaoTratamento { get; set; }

    public int? ModismosAlimentares { get; set; }

    public bool? FlgIntoleanciaAlimentar { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DescIntoleranciaAlimentar { get; set; }

    public int? FaseObesidadePerdaPeso { get; set; }

    public bool? FlgPerdaGanhoPeso { get; set; }

    public int? PesoMax { get; set; }

    public int? PesoMaxIdade { get; set; }

    public int? PesoMin { get; set; }

    public int? PesoMinIdade { get; set; }

    public bool? FlgDietas { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DescDietas { get; set; }

    public bool? FlgMedicamentos { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DescMedicamentos { get; set; }

    public bool? FlgExercicios { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DescExercicios { get; set; }

    public bool? FlgOutros { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DescOutros { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Obs { get; set; }

    [ForeignKey("IdPaciente")]
    [InverseProperty("TbHistoricoAlimentarNutricional")]
    public virtual TbPaciente IdPacienteNavigation { get; set; }
}