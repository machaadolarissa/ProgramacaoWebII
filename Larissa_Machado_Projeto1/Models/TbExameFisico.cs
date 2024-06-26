﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Larissa_Machado_Projeto1.Models;

[Table("tbExameFisico")]
[Index("IdHoraPacienteProfissional", Name = "IX_tbExameFisico_IdHoraPaciente_Profissional")]
public partial class TbExameFisico
{
    [Key]
    public int IdExameFisico { get; set; }

    [Column("IdHoraPaciente_Profissional")]
    public int? IdHoraPacienteProfissional { get; set; }

    [Column("SNC")]
    public int? Snc { get; set; }

    public int? AtividadeFisica { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string TipoAtividadeFisica { get; set; }

    public int? Frequencia { get; set; }

    public int? HoraPreferido { get; set; }

    public int? Tempo { get; set; }

    public bool? FlgDenticaoCompleta { get; set; }

    public bool? FlgDenticaoIncompleta { get; set; }

    public bool? FlgDenticaoAusente { get; set; }

    public bool? FlgDenticaoProtese { get; set; }

    public int? Mastigacao { get; set; }

    public int? Peristalse { get; set; }

    public int? Fumante { get; set; }

    public int? FrequenciaCardiaca { get; set; }

    [Column("PA")]
    [StringLength(30)]
    [Unicode(false)]
    public string Pa { get; set; }

    public int? FrequenciaRespiratoria { get; set; }

    public int? Temperatura { get; set; }

    public int? Glicemia { get; set; }

    public int? Diurese { get; set; }

    public int? TipoDiurese { get; set; }

    public int? Evacuacao { get; set; }

    public int? TipoEvacuacao { get; set; }

    public int? Ingestao { get; set; }

    public int? Excrecao { get; set; }

    public bool? FlgXerostomia { get; set; }

    public bool? FlgSialorreia { get; set; }

    public bool? FlgUlcerasBucais { get; set; }

    public bool? FlgNauseas { get; set; }

    public bool? FlgVomitos { get; set; }

    public bool? FlgDisfagia { get; set; }

    public bool? FlgOdinofagia { get; set; }

    public bool? FlgFistula { get; set; }

    public bool? FlgOral { get; set; }

    public bool? FlgOralEnteral { get; set; }

    public bool? FlgEnteralExclusiva { get; set; }

    public bool? FlgEnteralParental { get; set; }

    public bool? FlgParentalExclusiva { get; set; }

    public bool? FlgParentalOral { get; set; }

    public int? TipoAcesso { get; set; }

    public bool? FlgGastrico { get; set; }

    public bool? FlgTranspilorica { get; set; }

    public bool? FlgDuodenal { get; set; }

    public bool? FlgJejunal { get; set; }

    public bool? FlgHiperemia { get; set; }

    public bool? FlgSaidaSecrecao { get; set; }

    public bool? FlgPresencaFeridas { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Obs { get; set; }

    [ForeignKey("IdHoraPacienteProfissional")]
    [InverseProperty("TbExameFisico")]
    public virtual TbHoraPacienteProfissional IdHoraPacienteProfissionalNavigation { get; set; }
}