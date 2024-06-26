﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Larissa_Machado_Projeto1.Models;

[Table("tbGrupoPatologico_X_Patologia")]
[Index("IdGrupoPatologico", Name = "IX_tbGrupoPatologico_X_Patologia_IdGrupoPatologico")]
[Index("IdPatologia", Name = "IX_tbGrupoPatologico_X_Patologia_IdPatologia")]
public partial class TbGrupoPatologicoXPatologia
{
    [Key]
    [Column("IdPatologia_X_GrupoPatologico")]
    public int IdPatologiaXGrupoPatologico { get; set; }

    public int IdGrupoPatologico { get; set; }

    public int IdPatologia { get; set; }

    [ForeignKey("IdGrupoPatologico")]
    [InverseProperty("TbGrupoPatologicoXPatologia")]
    public virtual TbGrupoPatologico IdGrupoPatologicoNavigation { get; set; }

    [ForeignKey("IdPatologia")]
    [InverseProperty("TbGrupoPatologicoXPatologia")]
    public virtual TbPatologia IdPatologiaNavigation { get; set; }
}