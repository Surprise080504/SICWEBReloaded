using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SICDBWEB_MYSContext : DbContext
    {
        public SICDBWEB_MYSContext()
        {
        }

        public SICDBWEB_MYSContext(DbContextOptions<SICDBWEB_MYSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SicTAlmacen> SicTAlmacens { get; set; }
        public virtual DbSet<SicTAlmacenCentroCosto> SicTAlmacenCentroCostos { get; set; }
        public virtual DbSet<SicTBoletaDetalle> SicTBoletaDetalles { get; set; }
        public virtual DbSet<SicTBoletaDetalle1> SicTBoletaDetalles1 { get; set; }
        public virtual DbSet<SicTBoletum> SicTBoleta { get; set; }
        public virtual DbSet<SicTBoletum1> SicTBoleta1 { get; set; }
        public virtual DbSet<SicTCategorium> SicTCategoria { get; set; }
        public virtual DbSet<SicTCliContacCargo> SicTCliContacCargos { get; set; }
        public virtual DbSet<SicTCliContacto> SicTCliContactos { get; set; }
        public virtual DbSet<SicTCliDireccion> SicTCliDireccions { get; set; }
        public virtual DbSet<SicTCliNombCom> SicTCliNombComs { get; set; }
        public virtual DbSet<SicTCliRsHistorico> SicTCliRsHistoricos { get; set; }
        public virtual DbSet<SicTCliScoring> SicTCliScorings { get; set; }
        public virtual DbSet<SicTCliente> SicTClientes { get; set; }
        public virtual DbSet<SicTColabArea> SicTColabAreas { get; set; }
        public virtual DbSet<SicTColabCargo> SicTColabCargos { get; set; }
        public virtual DbSet<SicTColaborador> SicTColaboradors { get; set; }
        public virtual DbSet<SicTColor> SicTColors { get; set; }
        public virtual DbSet<SicTConcepto> SicTConceptos { get; set; }
        public virtual DbSet<SicTDepartamento> SicTDepartamentos { get; set; }
        public virtual DbSet<SicTDistrito> SicTDistritos { get; set; }
        public virtual DbSet<SicTEmpCentroCosto> SicTEmpCentroCostos { get; set; }
        public virtual DbSet<SicTEmpDireccion> SicTEmpDireccions { get; set; }
        public virtual DbSet<SicTEmpresa> SicTEmpresas { get; set; }
        public virtual DbSet<SicTEstilo> SicTEstilos { get; set; }
        public virtual DbSet<SicTEstiloProceso> SicTEstiloProcesos { get; set; }
        public virtual DbSet<SicTEstiloProcesoDetalle> SicTEstiloProcesoDetalles { get; set; }
        public virtual DbSet<SicTFactura> SicTFacturas { get; set; }
        public virtual DbSet<SicTFactura1> SicTFacturas1 { get; set; }
        public virtual DbSet<SicTFacturaDetalle> SicTFacturaDetalles { get; set; }
        public virtual DbSet<SicTFacturaDetalle1> SicTFacturaDetalles1 { get; set; }
        public virtual DbSet<SicTIgv> SicTIgvs { get; set; }
        public virtual DbSet<SicTImpresora> SicTImpresoras { get; set; }
        public virtual DbSet<SicTItem> SicTItems { get; set; }
        public virtual DbSet<SicTItemAlmacen> SicTItemAlmacens { get; set; }
        public virtual DbSet<SicTItemFamilium> SicTItemFamilia { get; set; }
        public virtual DbSet<SicTItemSubFamilium> SicTItemSubFamilia { get; set; }
        public virtual DbSet<SicTMenu> SicTMenus { get; set; }
        public virtual DbSet<SicTMovEstado> SicTMovEstados { get; set; }
        public virtual DbSet<SicTMovimientoEntradaDetalle> SicTMovimientoEntradaDetalles { get; set; }
        public virtual DbSet<SicTMovimientoEntradum> SicTMovimientoEntrada { get; set; }
        public virtual DbSet<SicTMovimientoSalidaDetalle> SicTMovimientoSalidaDetalles { get; set; }
        public virtual DbSet<SicTMovimientoSalidum> SicTMovimientoSalida { get; set; }
        public virtual DbSet<SicTNombCom> SicTNombComs { get; set; }
        public virtual DbSet<SicTOdcClase> SicTOdcClases { get; set; }
        public virtual DbSet<SicTOdcEstado> SicTOdcEstados { get; set; }
        public virtual DbSet<SicTOpcion> SicTOpcions { get; set; }
        public virtual DbSet<SicTOrdenDeCompra> SicTOrdenDeCompras { get; set; }
        public virtual DbSet<SicTOrdenDeCompraDet> SicTOrdenDeCompraDets { get; set; }
        public virtual DbSet<SicTParametro> SicTParametros { get; set; }
        public virtual DbSet<SicTParametroDet> SicTParametroDets { get; set; }
        public virtual DbSet<SicTPerfil> SicTPerfils { get; set; }
        public virtual DbSet<SicTPerfilMenu> SicTPerfilMenus { get; set; }
        public virtual DbSet<SicTPerfilOpcion> SicTPerfilOpcions { get; set; }
        public virtual DbSet<SicTProceso> SicTProcesos { get; set; }
        public virtual DbSet<SicTProductoPartidum> SicTProductoPartida { get; set; }
        public virtual DbSet<SicTProvincium> SicTProvincia { get; set; }
        public virtual DbSet<SicTSegmento> SicTSegmentos { get; set; }
        public virtual DbSet<SicTTalla> SicTTallas { get; set; }
        public virtual DbSet<SicTTasaCambio> SicTTasaCambios { get; set; }
        public virtual DbSet<SicTUnidadMedidum> SicTUnidadMedida { get; set; }
        public virtual DbSet<SicTUsuario> SicTUsuarios { get; set; }
        public virtual DbSet<SicTUsuarioOpcion> SicTUsuarioOpcions { get; set; }
        public virtual DbSet<SicTUsuarioPerfil> SicTUsuarioPerfils { get; set; }
        public virtual DbSet<SicTVenEstado> SicTVenEstados { get; set; }
        public virtual DbSet<SicTVentaDetalle> SicTVentaDetalles { get; set; }
        public virtual DbSet<SicTVentum> SicTVenta { get; set; }
        public virtual DbSet<SicTZonaReparto> SicTZonaRepartos { get; set; }
        public virtual DbSet<SicTZonaRepartoLugar> SicTZonaRepartoLugars { get; set; }
        public virtual DbSet<SicVwClienteListum> SicVwClienteLista { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-M0J3NN5;Database=SICDBWEB_MYS;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<SicTAlmacen>(entity =>
            {
                entity.HasKey(e => e.AlmCIid);

                entity.ToTable("SIC_T_ALMACEN", "Mantenimiento");

                entity.Property(e => e.AlmCIid).HasColumnName("alm_c_iid");

                entity.Property(e => e.AlmCBactivo).HasColumnName("alm_c_bactivo");

                entity.Property(e => e.AlmCVdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("alm_c_vdesc");
            });

            modelBuilder.Entity<SicTAlmacenCentroCosto>(entity =>
            {
                entity.HasKey(e => e.AlmCstCIid);

                entity.ToTable("SIC_T_ALMACEN_CENTRO_COSTO");

                entity.Property(e => e.AlmCstCIid).HasColumnName("alm_cst_c_iid");

                entity.Property(e => e.AlmCstCIidAlmacen).HasColumnName("alm_cst_c_iid_almacen");

                entity.Property(e => e.AlmCstCIidCentroCosto).HasColumnName("alm_cst_c_iid_centro_costo");

                entity.HasOne(d => d.AlmCstCIidAlmacenNavigation)
                    .WithMany(p => p.SicTAlmacenCentroCostos)
                    .HasForeignKey(d => d.AlmCstCIidAlmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ALMACEN_CENTRO_COSTO_SIC_T_ALMACEN");

                entity.HasOne(d => d.AlmCstCIidCentroCostoNavigation)
                    .WithMany(p => p.SicTAlmacenCentroCostos)
                    .HasForeignKey(d => d.AlmCstCIidCentroCosto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ALMACEN_CENTRO_COSTO_SIC_T_EMP_CENTRO_COSTO");
            });

            modelBuilder.Entity<SicTBoletaDetalle>(entity =>
            {
                entity.HasKey(e => e.BolDetCIid);

                entity.ToTable("SIC_T_BOLETA_DETALLE", "Facturacion");

                entity.Property(e => e.BolDetCIid).HasColumnName("bol_det_c_iid");

                entity.Property(e => e.BolCIid).HasColumnName("bol_c_iid");

                entity.Property(e => e.BolDetCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_det_c_ecantidad");

                entity.Property(e => e.BolDetCEpreciotot)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_det_c_epreciotot");

                entity.Property(e => e.BolDetCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_det_c_epreciounit");

                entity.Property(e => e.BolDetCIitem).HasColumnName("bol_det_c_iitem");

                entity.HasOne(d => d.BolCI)
                    .WithMany(p => p.SicTBoletaDetalles)
                    .HasForeignKey(d => d.BolCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_BOLETA_DETALLE_SIC_T_BOLETA");

                entity.HasOne(d => d.BolDetCIitemNavigation)
                    .WithMany(p => p.SicTBoletaDetalles)
                    .HasForeignKey(d => d.BolDetCIitem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_BOLETA_DETALLE_SIC_T_ITEM1");
            });

            modelBuilder.Entity<SicTBoletaDetalle1>(entity =>
            {
                entity.HasKey(e => e.BolDetCIid);

                entity.ToTable("SIC_T_BOLETA_DETALLE", "Venta");

                entity.Property(e => e.BolDetCIid)
                    .ValueGeneratedNever()
                    .HasColumnName("bol_det_c_iid");

                entity.Property(e => e.BolCIid).HasColumnName("bol_c_iid");

                entity.Property(e => e.BolDetCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_det_c_ecantidad");

                entity.Property(e => e.BolDetCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_det_c_epreciounit");

                entity.Property(e => e.BolDetCIitem).HasColumnName("bol_det_c_iitem");

                entity.HasOne(d => d.BolCI)
                    .WithMany(p => p.SicTBoletaDetalle1s)
                    .HasForeignKey(d => d.BolCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_BOLETA_DETALLE_SIC_T_BOLETA");
            });

            modelBuilder.Entity<SicTBoletum>(entity =>
            {
                entity.HasKey(e => e.BolCIid);

                entity.ToTable("SIC_T_BOLETA", "Facturacion");

                entity.Property(e => e.BolCIid).HasColumnName("bol_c_iid");

                entity.Property(e => e.BolCBimpreso).HasColumnName("bol_c_bimpreso");

                entity.Property(e => e.BolCEigv)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_eigv");

                entity.Property(e => e.BolCEigvcal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_eigvcal");

                entity.Property(e => e.BolCEsubtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_esubtotal");

                entity.Property(e => e.BolCEtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_etotal");

                entity.Property(e => e.BolCImoneda).HasColumnName("bol_c_imoneda");

                entity.Property(e => e.BolCIventa).HasColumnName("bol_c_iventa");

                entity.Property(e => e.BolCNumero).HasColumnName("bol_c_numero");

                entity.Property(e => e.BolCSerie)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("bol_c_serie");

                entity.Property(e => e.BolCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bol_c_vdescmoneda");

                entity.Property(e => e.BolCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("bol_c_zfecharegistro");

                entity.HasOne(d => d.BolCIventaNavigation)
                    .WithMany(p => p.SicTBoleta)
                    .HasForeignKey(d => d.BolCIventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_BOLETA_SIC_T_VENTA");
            });

            modelBuilder.Entity<SicTBoletum1>(entity =>
            {
                entity.HasKey(e => e.BolCIid);

                entity.ToTable("SIC_T_BOLETA", "Venta");

                entity.Property(e => e.BolCIid).HasColumnName("bol_c_iid");

                entity.Property(e => e.BolCEigv)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_eigv");

                entity.Property(e => e.BolCEigvcal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_eigvcal");

                entity.Property(e => e.BolCEsubtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_esubtotal");

                entity.Property(e => e.BolCEtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("bol_c_etotal");

                entity.Property(e => e.BolCImoneda).HasColumnName("bol_c_imoneda");

                entity.Property(e => e.BolCIventa).HasColumnName("bol_c_iventa");

                entity.Property(e => e.BolCNumero).HasColumnName("bol_c_numero");

                entity.Property(e => e.BolCSerie)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("bol_c_serie");

                entity.Property(e => e.BolCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bol_c_vdescmoneda");

                entity.Property(e => e.BolCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("bol_c_zfecharegistro");

                entity.HasOne(d => d.BolCIventaNavigation)
                    .WithMany(p => p.SicTBoletum1s)
                    .HasForeignKey(d => d.BolCIventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_BOLETA_SIC_T_VENTA");
            });

            modelBuilder.Entity<SicTCategorium>(entity =>
            {
                entity.HasKey(e => e.CateCVid);

                entity.ToTable("SIC_T_CATEGORIA", "Confeccion");

                entity.Property(e => e.CateCVid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cate_c_vid");

                entity.Property(e => e.CateCVcodigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cate_c_vcodigo");

                entity.Property(e => e.CateCVdescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cate_c_vdescripcion");
            });

            modelBuilder.Entity<SicTCliContacCargo>(entity =>
            {
                entity.HasKey(e => e.CliContacCargoCYid);

                entity.ToTable("SIC_T_CLI_CONTAC_CARGO", "Mantenimiento");

                entity.Property(e => e.CliContacCargoCYid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("cli_contac_cargo_c_yid");

                entity.Property(e => e.CliContacCargoCVnomb)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_cargo_c_vnomb");
            });

            modelBuilder.Entity<SicTCliContacto>(entity =>
            {
                entity.HasKey(e => e.CliContacCIid);

                entity.ToTable("SIC_T_CLI_CONTACTO", "Mantenimiento");

                entity.Property(e => e.CliContacCIid).HasColumnName("cli_contac_c_iid");

                entity.Property(e => e.CliCVdocId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.CliContacCBactivo).HasColumnName("cli_contac_c_bactivo");

                entity.Property(e => e.CliContacCCcel)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_ccel");

                entity.Property(e => e.CliContacCCdocId)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_cdoc_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CliContacCCtlf)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_ctlf");

                entity.Property(e => e.CliContacCDfecCump)
                    .HasColumnType("date")
                    .HasColumnName("cli_contac_c_dfec_cump");

                entity.Property(e => e.CliContacCVapeMat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_vape_mat");

                entity.Property(e => e.CliContacCVapePat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_vape_pat");

                entity.Property(e => e.CliContacCVcorreo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_vcorreo");

                entity.Property(e => e.CliContacCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_vnomb");

                entity.Property(e => e.CliContacCVobserv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cli_contac_c_vobserv");

                entity.Property(e => e.CliContacCargoCYid).HasColumnName("cli_contac_cargo_c_yid");

                entity.HasOne(d => d.CliCVdoc)
                    .WithMany(p => p.SicTCliContactos)
                    .HasForeignKey(d => d.CliCVdocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_CLI_CONTACTO_SIC_T_CLIENTE");

                entity.HasOne(d => d.CliContacCargoCY)
                    .WithMany(p => p.SicTCliContactos)
                    .HasForeignKey(d => d.CliContacCargoCYid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_CLI_CONTACTO_SIC_T_CLI_CONTAC_CARGO");
            });

            modelBuilder.Entity<SicTCliDireccion>(entity =>
            {
                entity.HasKey(e => new { e.CliCVdocId, e.CliDirecCIid });

                entity.ToTable("SIC_T_CLI_DIRECCION", "Mantenimiento");

                entity.Property(e => e.CliCVdocId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.CliDirecCIid).HasColumnName("cli_direc_c_iid");

                entity.Property(e => e.CliDirecCBactivo).HasColumnName("cli_direc_c_bactivo");

                entity.Property(e => e.CliDirecCCtipo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cli_direc_c_ctipo")
                    .IsFixedLength(true);

                entity.Property(e => e.CliDirecCCzonarep)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cli_direc_c_czonarep")
                    .IsFixedLength(true);

                entity.Property(e => e.CliDirecCVdirec)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_direc_c_vdirec");

                entity.Property(e => e.DistCCcodUbig)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("dist_c_ccod_ubig")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CliCVdoc)
                    .WithMany(p => p.SicTCliDireccions)
                    .HasForeignKey(d => d.CliCVdocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_CLI_DIRECCION_SIC_T_CLIENTE");

                entity.HasOne(d => d.DistCCcodUbigNavigation)
                    .WithMany(p => p.SicTCliDireccions)
                    .HasForeignKey(d => d.DistCCcodUbig)
                    .HasConstraintName("FK_SIC_T_CLI_DIRECCION_SIC_T_DISTRITO");
            });

            modelBuilder.Entity<SicTCliNombCom>(entity =>
            {
                entity.HasKey(e => new { e.CliCVdocId, e.NombComCIid });

                entity.ToTable("SIC_T_CLI_NOMB_COM", "Mantenimiento");

                entity.Property(e => e.CliCVdocId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.NombComCIid).HasColumnName("nomb_com_c_iid");

                entity.HasOne(d => d.CliCVdoc)
                    .WithMany(p => p.SicTCliNombComs)
                    .HasForeignKey(d => d.CliCVdocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_CLI_NOMB_COM_SIC_T_CLIENTE");
            });

            modelBuilder.Entity<SicTCliRsHistorico>(entity =>
            {
                entity.HasKey(e => e.CliRsHCIid);

                entity.ToTable("SIC_T_CLI_RS_HISTORICO", "Mantenimiento");

                entity.Property(e => e.CliRsHCIid).HasColumnName("cli_rs_h_c_iid");

                entity.Property(e => e.CliCVdocId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.CliRsHCDfecReg)
                    .HasColumnType("datetime")
                    .HasColumnName("cli_rs_h_c_dfec_reg");

                entity.Property(e => e.CliRsHCVrazSoc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_rs_h_c_vraz_soc");
            });

            modelBuilder.Entity<SicTCliScoring>(entity =>
            {
                entity.HasKey(e => e.CliScorCCletra);

                entity.ToTable("SIC_T_CLI_SCORING", "Mantenimiento");

                entity.Property(e => e.CliScorCCletra)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cli_scor_c_cletra")
                    .IsFixedLength(true);

                entity.Property(e => e.CliScorCVobserv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cli_scor_c_vobserv");
            });

            modelBuilder.Entity<SicTCliente>(entity =>
            {
                entity.HasKey(e => e.CliCVdocId);

                entity.ToTable("SIC_T_CLIENTE", "Mantenimiento");

                entity.Property(e => e.CliCVdocId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.CliCBactivo)
                    .HasColumnName("cli_c_bactivo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CliCBcliente).HasColumnName("cli_c_bcliente");

                entity.Property(e => e.CliCBproveedor).HasColumnName("cli_c_bproveedor");

                entity.Property(e => e.CliCBtipoPers).HasColumnName("cli_c_btipo_pers");

                entity.Property(e => e.CliCCtlf)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_ctlf");

                entity.Property(e => e.CliCDfecAniv)
                    .HasColumnType("date")
                    .HasColumnName("cli_c_dfec_aniv");

                entity.Property(e => e.CliCDfecConst)
                    .HasColumnType("date")
                    .HasColumnName("cli_c_dfec_const");

                entity.Property(e => e.CliCDfechaactualiza)
                    .HasColumnType("datetime")
                    .HasColumnName("cli_c_dfechaactualiza");

                entity.Property(e => e.CliCDfecharegistra)
                    .HasColumnType("datetime")
                    .HasColumnName("cli_c_dfecharegistra");

                entity.Property(e => e.CliCVpartida)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vpartida");

                entity.Property(e => e.CliCVrazSoc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vraz_soc");

                entity.Property(e => e.CliCVrubro)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vrubro");

                entity.Property(e => e.CliScorCCletra)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cli_scor_c_cletra")
                    .IsFixedLength(true);

                entity.Property(e => e.ColabCCdocId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_cdoc_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ZonaRepCYid).HasColumnName("zona_rep_c_yid");

                entity.HasOne(d => d.CliScorCCletraNavigation)
                    .WithMany(p => p.SicTClientes)
                    .HasForeignKey(d => d.CliScorCCletra)
                    .HasConstraintName("FK_SIC_T_CLIENTE_SIC_T_CLI_SCORING");

                entity.HasOne(d => d.ColabCCdoc)
                    .WithMany(p => p.SicTClientes)
                    .HasForeignKey(d => d.ColabCCdocId)
                    .HasConstraintName("FK_SIC_T_CLIENTE_SIC_T_COLABORADOR");

                entity.HasOne(d => d.ZonaRepCY)
                    .WithMany(p => p.SicTClientes)
                    .HasForeignKey(d => d.ZonaRepCYid)
                    .HasConstraintName("FK_SIC_T_CLIENTE_SIC_T_ZONA_REPARTO");
            });

            modelBuilder.Entity<SicTColabArea>(entity =>
            {
                entity.HasKey(e => e.ColabAreaCYid);

                entity.ToTable("SIC_T_COLAB_AREA", "Mantenimiento");

                entity.Property(e => e.ColabAreaCYid).HasColumnName("colab_area_c_yid");

                entity.Property(e => e.ColabAreaCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("colab_area_c_vnomb");
            });

            modelBuilder.Entity<SicTColabCargo>(entity =>
            {
                entity.HasKey(e => e.ColabCargoCYid);

                entity.ToTable("SIC_T_COLAB_CARGO", "Mantenimiento");

                entity.Property(e => e.ColabCargoCYid).HasColumnName("colab_cargo_c_yid");

                entity.Property(e => e.ColabCargoCVnomb)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("colab_cargo_c_vnomb");
            });

            modelBuilder.Entity<SicTColaborador>(entity =>
            {
                entity.HasKey(e => e.ColabCCdocId);

                entity.ToTable("SIC_T_COLABORADOR", "Mantenimiento");

                entity.Property(e => e.ColabCCdocId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_cdoc_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ColabAreaCYid).HasColumnName("colab_area_c_yid");

                entity.Property(e => e.ColabCCusuRed)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_cusu_red")
                    .IsFixedLength(true);

                entity.Property(e => e.ColabCVapeMat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_vape_mat");

                entity.Property(e => e.ColabCVapePat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_vape_pat");

                entity.Property(e => e.ColabCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("colab_c_vnomb");

                entity.Property(e => e.ColabCargoCYid).HasColumnName("colab_cargo_c_yid");

                entity.HasOne(d => d.ColabAreaCY)
                    .WithMany(p => p.SicTColaboradors)
                    .HasForeignKey(d => d.ColabAreaCYid)
                    .HasConstraintName("FK_SIC_T_COLABORADOR_SIC_T_COLAB_AREA");

                entity.HasOne(d => d.ColabCargoCY)
                    .WithMany(p => p.SicTColaboradors)
                    .HasForeignKey(d => d.ColabCargoCYid)
                    .HasConstraintName("FK_SIC_T_COLABORADOR_SIC_T_COLAB_CARGO");
            });

            modelBuilder.Entity<SicTColor>(entity =>
            {
                entity.HasKey(e => e.ColorCVid);

                entity.ToTable("SIC_T_COLOR", "Confeccion");

                entity.Property(e => e.ColorCVid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("color_c_vid");

                entity.Property(e => e.ColorCVcodigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("color_c_vcodigo");

                entity.Property(e => e.ColorCVdescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("color_c_vdescripcion");
            });

            modelBuilder.Entity<SicTConcepto>(entity =>
            {
                entity.HasKey(e => e.ConCIid);

                entity.ToTable("SIC_T_CONCEPTO", "General");

                entity.Property(e => e.ConCIid)
                    .ValueGeneratedNever()
                    .HasColumnName("con_c_iid");

                entity.Property(e => e.ConCVdes)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con_c_vdes");
            });

            modelBuilder.Entity<SicTDepartamento>(entity =>
            {
                entity.HasKey(e => e.DepaCCcod);

                entity.ToTable("SIC_T_DEPARTAMENTO", "Mantenimiento");

                entity.Property(e => e.DepaCCcod)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("depa_c_ccod")
                    .IsFixedLength(true);

                entity.Property(e => e.DepaCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("depa_c_vnomb");
            });

            modelBuilder.Entity<SicTDistrito>(entity =>
            {
                entity.HasKey(e => e.DistCCcodUbig);

                entity.ToTable("SIC_T_DISTRITO", "Mantenimiento");

                entity.Property(e => e.DistCCcodUbig)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("dist_c_ccod_ubig")
                    .IsFixedLength(true);

                entity.Property(e => e.DistCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dist_c_vnomb");

                entity.Property(e => e.ProvCCcod)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("prov_c_ccod")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ProvCCcodNavigation)
                    .WithMany(p => p.SicTDistritos)
                    .HasForeignKey(d => d.ProvCCcod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_DISTRITO_SIC_T_PROVINCIA");
            });

            modelBuilder.Entity<SicTEmpCentroCosto>(entity =>
            {
                entity.HasKey(e => e.EmpCstCIid)
                    .HasName("PK_SIC_T_EMP_CENTRO_COSTO_1");

                entity.ToTable("SIC_T_EMP_CENTRO_COSTO", "Mantenimiento");

                entity.Property(e => e.EmpCstCIid).HasColumnName("emp_cst_c_iid");

                entity.Property(e => e.EmpCstCBactivo).HasColumnName("emp_cst_c_bactivo");

                entity.Property(e => e.EmpCstCInumeroboleta).HasColumnName("emp_cst_c_inumeroboleta");

                entity.Property(e => e.EmpCstCInumerofact).HasColumnName("emp_cst_c_inumerofact");

                entity.Property(e => e.EmpCstCVdesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_cst_c_vdesc");

                entity.Property(e => e.EmpCstCVserieboleta)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("emp_cst_c_vserieboleta");

                entity.Property(e => e.EmpCstCVseriefactura)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("emp_cst_c_vseriefactura");
            });

            modelBuilder.Entity<SicTEmpDireccion>(entity =>
            {
                entity.HasKey(e => e.EmpDirCIid);

                entity.ToTable("SIC_T_EMP_DIRECCION", "Mantenimiento");

                entity.Property(e => e.EmpDirCIid).HasColumnName("emp_dir_c_iid");

                entity.Property(e => e.EmpDirCBactivo).HasColumnName("emp_dir_c_bactivo");

                entity.Property(e => e.EmpDirCCcodUbig)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("emp_dir_c_ccod_ubig")
                    .IsFixedLength(true);

                entity.Property(e => e.EmpDirCIidCentrocosto).HasColumnName("emp_dir_c_iid_centrocosto");

                entity.Property(e => e.EmpDirCItipodirec).HasColumnName("emp_dir_c_itipodirec");

                entity.Property(e => e.EmpDirCVdireccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_dir_c_vdireccion");

                entity.Property(e => e.EmpDirCVtipodirec)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_dir_c_vtipodirec");

                entity.HasOne(d => d.EmpDirCIidCentrocostoNavigation)
                    .WithMany(p => p.SicTEmpDireccions)
                    .HasForeignKey(d => d.EmpDirCIidCentrocosto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_EMP_DIRECCION_SIC_T_EMP_CENTRO_COSTO");
            });

            modelBuilder.Entity<SicTEmpresa>(entity =>
            {
                entity.HasKey(e => e.EmpCIid);

                entity.ToTable("SIC_T_EMPRESA", "Mantenimiento");

                entity.Property(e => e.EmpCIid).HasColumnName("emp_c_iid");

                entity.Property(e => e.EmpCVrazonsocial)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_c_vrazonsocial");

                entity.Property(e => e.EmpCVruc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_c_vruc");
            });

            modelBuilder.Entity<SicTEstilo>(entity =>
            {
                entity.HasKey(e => e.EstiCIid);

                entity.ToTable("SIC_T_ESTILO", "Confeccion");

                entity.HasIndex(e => e.EstiCVcodigo, "UQ_SIC_T_ESTILO_CODIGO")
                    .IsUnique();

                entity.HasIndex(e => e.EstiCVnombre, "UQ_SIC_T_ESTILO_NOMBRE")
                    .IsUnique();

                entity.Property(e => e.EstiCIid).HasColumnName("esti_c_iid");

                entity.Property(e => e.CateCVid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cate_c_vid");

                entity.Property(e => e.ColorCVid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("color_c_vid");

                entity.Property(e => e.EstiCVcodigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("esti_c_vcodigo");

                entity.Property(e => e.EstiCVdescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("esti_c_vdescripcion");

                entity.Property(e => e.EstiCVnombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("esti_c_vnombre");

                entity.Property(e => e.ItmCIid).HasColumnName("itm_c_iid");

                entity.Property(e => e.TallaCVid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("talla_c_vid");

                entity.HasOne(d => d.CateCV)
                    .WithMany(p => p.SicTEstilos)
                    .HasForeignKey(d => d.CateCVid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO_CATEGORIA");

                entity.HasOne(d => d.ColorCV)
                    .WithMany(p => p.SicTEstilos)
                    .HasForeignKey(d => d.ColorCVid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO_COLOR");

                entity.HasOne(d => d.ItmCI)
                    .WithMany(p => p.SicTEstilos)
                    .HasForeignKey(d => d.ItmCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO_ITEM");

                entity.HasOne(d => d.TallaCV)
                    .WithMany(p => p.SicTEstilos)
                    .HasForeignKey(d => d.TallaCVid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO_TALLA");
            });

            modelBuilder.Entity<SicTEstiloProceso>(entity =>
            {
                entity.HasKey(e => e.EstiProcesoCIid);

                entity.ToTable("SIC_T_ESTILO_PROCESO", "Confeccion");

                entity.Property(e => e.EstiProcesoCIid).HasColumnName("esti_proceso_c_iid");

                entity.Property(e => e.EstiCIid).HasColumnName("esti_c_iid");

                entity.Property(e => e.EstiProcesoCEcosto)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("esti_proceso_c_ecosto");

                entity.Property(e => e.ProcCVid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("proc_c_vid");

                entity.HasOne(d => d.EstiCI)
                    .WithMany(p => p.SicTEstiloProcesos)
                    .HasForeignKey(d => d.EstiCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO");

                entity.HasOne(d => d.ProcCV)
                    .WithMany(p => p.SicTEstiloProcesos)
                    .HasForeignKey(d => d.ProcCVid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PROCESO");
            });

            modelBuilder.Entity<SicTEstiloProcesoDetalle>(entity =>
            {
                entity.HasKey(e => e.EstiProcDetalleCIid);

                entity.ToTable("SIC_T_ESTILO_PROCESO_DETALLE", "Confeccion");

                entity.Property(e => e.EstiProcDetalleCIid).HasColumnName("esti_proc_detalle_c_iid");

                entity.Property(e => e.EstiProcDetalleCEcosto)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("esti_proc_detalle_c_ecosto");

                entity.Property(e => e.EstiProcDetalleCIsegundos)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("esti_proc_detalle_c_isegundos");

                entity.Property(e => e.EstiProcDetalleCVdescripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("esti_proc_detalle_c_vdescripcion");

                entity.Property(e => e.EstiProcDetalleCVmaquina)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("esti_proc_detalle_c_vmaquina");

                entity.Property(e => e.EstiProcesoCIid).HasColumnName("esti_proceso_c_iid");

                entity.Property(e => e.EstiProcesoCYorden).HasColumnName("esti_proceso_c_yorden");

                entity.HasOne(d => d.EstiProcesoCI)
                    .WithMany(p => p.SicTEstiloProcesoDetalles)
                    .HasForeignKey(d => d.EstiProcesoCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ESTILO_PROCESO_DETALLE");
            });

            modelBuilder.Entity<SicTFactura>(entity =>
            {
                entity.HasKey(e => e.FacCIid);

                entity.ToTable("SIC_T_FACTURA", "Facturacion");

                entity.Property(e => e.FacCIid).HasColumnName("fac_c_iid");

                entity.Property(e => e.FacCBimpreso).HasColumnName("fac_c_bimpreso");

                entity.Property(e => e.FacCEigv)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_eigv");

                entity.Property(e => e.FacCEigvcal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_eigvcal");

                entity.Property(e => e.FacCEsubtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_esubtotal");

                entity.Property(e => e.FacCEtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_etotal");

                entity.Property(e => e.FacCImoneda).HasColumnName("fac_c_imoneda");

                entity.Property(e => e.FacCIventa).HasColumnName("fac_c_iventa");

                entity.Property(e => e.FacCNumero).HasColumnName("fac_c_numero");

                entity.Property(e => e.FacCSerie)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("fac_c_serie");

                entity.Property(e => e.FacCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fac_c_vdescmoneda");

                entity.Property(e => e.FacCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fac_c_zfecharegistro");

                entity.HasOne(d => d.FacCIventaNavigation)
                    .WithMany(p => p.SicTFacturas)
                    .HasForeignKey(d => d.FacCIventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_FACTURA_SIC_T_VENTA");
            });

            modelBuilder.Entity<SicTFactura1>(entity =>
            {
                entity.HasKey(e => e.FacCIid);

                entity.ToTable("SIC_T_FACTURA", "Venta");

                entity.Property(e => e.FacCIid).HasColumnName("fac_c_iid");

                entity.Property(e => e.FacCEigv)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_eigv");

                entity.Property(e => e.FacCEigvcal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_eigvcal");

                entity.Property(e => e.FacCEsubtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_esubtotal");

                entity.Property(e => e.FacCEtotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_c_etotal");

                entity.Property(e => e.FacCImoneda).HasColumnName("fac_c_imoneda");

                entity.Property(e => e.FacCIventa).HasColumnName("fac_c_iventa");

                entity.Property(e => e.FacCNumero).HasColumnName("fac_c_numero");

                entity.Property(e => e.FacCSerie)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("fac_c_serie");

                entity.Property(e => e.FacCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fac_c_vdescmoneda");

                entity.Property(e => e.FacCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fac_c_zfecharegistro");

                entity.HasOne(d => d.FacCIventaNavigation)
                    .WithMany(p => p.SicTFactura1s)
                    .HasForeignKey(d => d.FacCIventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_FACTURA_SIC_T_VENTA");
            });

            modelBuilder.Entity<SicTFacturaDetalle>(entity =>
            {
                entity.HasKey(e => e.FacDetCIid);

                entity.ToTable("SIC_T_FACTURA_DETALLE", "Facturacion");

                entity.Property(e => e.FacDetCIid).HasColumnName("fac_det_c_iid");

                entity.Property(e => e.FacCIid).HasColumnName("fac_c_iid");

                entity.Property(e => e.FacDetCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_det_c_ecantidad");

                entity.Property(e => e.FacDetCEpreciotot)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_det_c_epreciotot");

                entity.Property(e => e.FacDetCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_det_c_epreciounit");

                entity.Property(e => e.FacDetCIitem).HasColumnName("fac_det_c_iitem");

                entity.HasOne(d => d.FacCI)
                    .WithMany(p => p.SicTFacturaDetalles)
                    .HasForeignKey(d => d.FacCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_FACTURA_DETALLE_SIC_T_FACTURA");

                entity.HasOne(d => d.FacDetCIitemNavigation)
                    .WithMany(p => p.SicTFacturaDetalles)
                    .HasForeignKey(d => d.FacDetCIitem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_FACTURA_DETALLE_SIC_T_ITEM");
            });

            modelBuilder.Entity<SicTFacturaDetalle1>(entity =>
            {
                entity.HasKey(e => e.FacDetCIid);

                entity.ToTable("SIC_T_FACTURA_DETALLE", "Venta");

                entity.Property(e => e.FacDetCIid)
                    .ValueGeneratedNever()
                    .HasColumnName("fac_det_c_iid");

                entity.Property(e => e.FacCIid).HasColumnName("fac_c_iid");

                entity.Property(e => e.FacDetCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_det_c_ecantidad");

                entity.Property(e => e.FacDetCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("fac_det_c_epreciounit");

                entity.Property(e => e.FacDetCIitem).HasColumnName("fac_det_c_iitem");

                entity.HasOne(d => d.FacCI)
                    .WithMany(p => p.SicTFacturaDetalle1s)
                    .HasForeignKey(d => d.FacCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_FACTURA_DETALLE_SIC_T_FACTURA");
            });

            modelBuilder.Entity<SicTIgv>(entity =>
            {
                entity.HasKey(e => e.IgvCIid);

                entity.ToTable("SIC_T_IGV", "Mantenimiento");

                entity.Property(e => e.IgvCIid).HasColumnName("igv_c_iid");

                entity.Property(e => e.IgvCDfin)
                    .HasColumnType("datetime")
                    .HasColumnName("igv_c_dfin");

                entity.Property(e => e.IgvCDinicio)
                    .HasColumnType("datetime")
                    .HasColumnName("igv_c_dinicio");

                entity.Property(e => e.IgvCEigv)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("igv_c_eigv");
            });

            modelBuilder.Entity<SicTImpresora>(entity =>
            {
                entity.HasKey(e => e.ImpCIid);

                entity.ToTable("SIC_T_IMPRESORA", "General");

                entity.Property(e => e.ImpCIid)
                    .ValueGeneratedNever()
                    .HasColumnName("imp_c_iid");

                entity.Property(e => e.ImpCVruta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imp_c_vruta");
            });

            modelBuilder.Entity<SicTItem>(entity =>
            {
                entity.HasKey(e => e.ItmCIid);

                entity.ToTable("SIC_T_ITEM", "Mantenimiento");

                entity.HasIndex(e => e.ItmCCcodigo, "UQ__SIC_T_IT__28CD5C006EF57B66")
                    .IsUnique();

                entity.Property(e => e.ItmCIid).HasColumnName("itm_c_iid");

                entity.Property(e => e.ItmCBactivo).HasColumnName("itm_c_bactivo");

                entity.Property(e => e.ItmCCcodigo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("itm_c_ccodigo");

                entity.Property(e => e.ItmCDprecioCompra)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("itm_c_dprecio_compra");

                entity.Property(e => e.ItmCDprecioVenta)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("itm_c_dprecio_venta");

                entity.Property(e => e.ItmCVdescripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("itm_c_vdescripcion");

                entity.Property(e => e.ProPartidaCIid).HasColumnName("pro_partida_c_iid");

                entity.Property(e => e.UndCYid).HasColumnName("und_c_yid");

                entity.HasOne(d => d.ProPartidaCI)
                    .WithMany(p => p.SicTItems)
                    .HasForeignKey(d => d.ProPartidaCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ITEM_PRODUCTO_PARTIDA");

                entity.HasOne(d => d.UndCY)
                    .WithMany(p => p.SicTItems)
                    .HasForeignKey(d => d.UndCYid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ITEM_SIC_T_UNIDAD_MEDIDA");
            });

            modelBuilder.Entity<SicTItemAlmacen>(entity =>
            {
                entity.HasKey(e => e.ItmAlmCIid);

                entity.ToTable("SIC_T_ITEM_ALMACEN", "Mantenimiento");

                entity.Property(e => e.ItmAlmCIid).HasColumnName("itm_alm_c_iid");

                entity.Property(e => e.AlmCIid).HasColumnName("alm_c_iid");

                entity.Property(e => e.ItmAlmCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("itm_alm_c_ecantidad");

                entity.Property(e => e.ItmCIid).HasColumnName("itm_c_iid");

                entity.HasOne(d => d.AlmCI)
                    .WithMany(p => p.SicTItemAlmacens)
                    .HasForeignKey(d => d.AlmCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ITEM_ALMACEN_SIC_T_ALMACEN");

                entity.HasOne(d => d.ItmCI)
                    .WithMany(p => p.SicTItemAlmacens)
                    .HasForeignKey(d => d.ItmCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ITEM_ALMACEN_SIC_T_ITEM");
            });

            modelBuilder.Entity<SicTItemFamilium>(entity =>
            {
                entity.HasKey(e => e.IfmCIid)
                    .HasName("PK_SIC_T_FAMILIA_ITEM");

                entity.ToTable("SIC_T_ITEM_FAMILIA", "Mantenimiento");

                entity.Property(e => e.IfmCIid).HasColumnName("ifm_c_iid");

                entity.Property(e => e.IfmCBactivo).HasColumnName("ifm_c_bactivo");

                entity.Property(e => e.IfmCDes)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ifm_c_des");

                entity.Property(e => e.SegmentoCYid).HasColumnName("segmento_c_yid");

                entity.HasOne(d => d.SegmentoCY)
                    .WithMany(p => p.SicTItemFamilia)
                    .HasForeignKey(d => d.SegmentoCYid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITEM_FAMILIA_SEGMENTO");
            });

            modelBuilder.Entity<SicTItemSubFamilium>(entity =>
            {
                entity.HasKey(e => e.IsfCIid)
                    .HasName("PK_SIC_T_SUB_FAMILIA_ITEM");

                entity.ToTable("SIC_T_ITEM_SUB_FAMILIA", "Mantenimiento");

                entity.Property(e => e.IsfCIid).HasColumnName("isf_c_iid");

                entity.Property(e => e.IsfCBactivo).HasColumnName("isf_c_bactivo");

                entity.Property(e => e.IsfCIfmIid).HasColumnName("isf_c_ifm_iid");

                entity.Property(e => e.IsfCVdesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("isf_c_vdesc");

                entity.HasOne(d => d.IsfCIfmI)
                    .WithMany(p => p.SicTItemSubFamilia)
                    .HasForeignKey(d => d.IsfCIfmIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ITEM_SUB_FAMILIA_SIC_T_ITEM_FAMILIA");
            });

            modelBuilder.Entity<SicTMenu>(entity =>
            {
                entity.HasKey(e => e.MenuCIid);

                entity.ToTable("SIC_T_MENU", "Seguridad");

                entity.Property(e => e.MenuCIid).HasColumnName("menu_c_iid");

                entity.Property(e => e.MenuCIidPadre).HasColumnName("menu_c_iid_padre");

                entity.Property(e => e.MenuCVnomb)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("menu_c_vnomb");

                entity.Property(e => e.MenuCVpagAsp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("menu_c_vpag_asp");

                entity.Property(e => e.MenuCYnivel).HasColumnName("menu_c_ynivel");

                entity.HasOne(d => d.MenuCIidPadreNavigation)
                    .WithMany(p => p.InverseMenuCIidPadreNavigation)
                    .HasForeignKey(d => d.MenuCIidPadre)
                    .HasConstraintName("FK_SIC_T_MENU_SIC_T_MENU");
            });

            modelBuilder.Entity<SicTMovEstado>(entity =>
            {
                entity.HasKey(e => e.MovEstadoIid);

                entity.ToTable("SIC_T_MOV_ESTADO", "Mantenimiento");

                entity.Property(e => e.MovEstadoIid).HasColumnName("mov_estado_iid");

                entity.Property(e => e.MovEstadoVdescrpcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mov_estado_vdescrpcion");
            });

            modelBuilder.Entity<SicTMovimientoEntradaDetalle>(entity =>
            {
                entity.HasKey(e => e.MveDetCIid);

                entity.ToTable("SIC_T_MOVIMIENTO_ENTRADA_DETALLE", "Mantenimiento");

                entity.Property(e => e.MveDetCIid).HasColumnName("mve_det_c_iid");

                entity.Property(e => e.MveCEcantPedida)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("mve_c_ecant_pedida");

                entity.Property(e => e.MveCEcantRecibida)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("mve_c_ecant_recibida");

                entity.Property(e => e.MveCIid).HasColumnName("mve_c_iid");

                entity.Property(e => e.MveCIocdetId).HasColumnName("mve_c_iocdet_id");

                entity.Property(e => e.MveCVdescripcionItem)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("mve_c_vdescripcion_item");

                entity.HasOne(d => d.MveCI)
                    .WithMany(p => p.SicTMovimientoEntradaDetalles)
                    .HasForeignKey(d => d.MveCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_SIC_T_MOVIMIENTO_ENTRADA");

                entity.HasOne(d => d.MveCIocdet)
                    .WithMany(p => p.SicTMovimientoEntradaDetalles)
                    .HasForeignKey(d => d.MveCIocdetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_SIC_T_ORDEN_DE_COMPRA_DET");
            });

            modelBuilder.Entity<SicTMovimientoEntradum>(entity =>
            {
                entity.HasKey(e => e.MveCIid);

                entity.ToTable("SIC_T_MOVIMIENTO_ENTRADA", "Mantenimiento");

                entity.Property(e => e.MveCIid).HasColumnName("mve_c_iid");

                entity.Property(e => e.MveCBactivo).HasColumnName("mve_c_bactivo");

                entity.Property(e => e.MveCBingresado).HasColumnName("mve_c_bingresado");

                entity.Property(e => e.MveCIestado).HasColumnName("mve_c_iestado");

                entity.Property(e => e.MveCIidalmacen).HasColumnName("mve_c_iidalmacen");

                entity.Property(e => e.MveCVdesestado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("mve_c_vdesestado");

                entity.Property(e => e.MveCVfacturacodigo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mve_c_vfacturacodigo");

                entity.Property(e => e.MveCVguiacodigo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mve_c_vguiacodigo");

                entity.Property(e => e.MveCVobservacion)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("mve_c_vobservacion");

                entity.Property(e => e.MveCZfacturafecha)
                    .HasColumnType("datetime")
                    .HasColumnName("mve_c_zfacturafecha");

                entity.Property(e => e.MveCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("mve_c_zfecharegistro");

                entity.Property(e => e.MveCZguiafecha)
                    .HasColumnType("datetime")
                    .HasColumnName("mve_c_zguiafecha");

                entity.Property(e => e.OdcCIid).HasColumnName("odc_c_iid");

                entity.HasOne(d => d.MveCIestadoNavigation)
                    .WithMany(p => p.SicTMovimientoEntrada)
                    .HasForeignKey(d => d.MveCIestado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_MOV_ESTADO");

                entity.HasOne(d => d.MveCIidalmacenNavigation)
                    .WithMany(p => p.SicTMovimientoEntrada)
                    .HasForeignKey(d => d.MveCIidalmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_ALMACEN");

                entity.HasOne(d => d.OdcCI)
                    .WithMany(p => p.SicTMovimientoEntrada)
                    .HasForeignKey(d => d.OdcCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_ORDEN_DE_COMPRA");
            });

            modelBuilder.Entity<SicTMovimientoSalidaDetalle>(entity =>
            {
                entity.HasKey(e => e.MvsDetCIid);

                entity.ToTable("SIC_T_MOVIMIENTO_SALIDA_DETALLE", "Almacen");

                entity.Property(e => e.MvsDetCIid).HasColumnName("mvs_det_c_iid");

                entity.Property(e => e.AlmCIid).HasColumnName("alm_c_iid");

                entity.Property(e => e.ItmCIid).HasColumnName("itm_c_iid");

                entity.Property(e => e.MvsCIid).HasColumnName("mvs_c_iid");

                entity.Property(e => e.MvsDetCEcant)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("mvs_det_c_ecant");

                entity.HasOne(d => d.AlmCI)
                    .WithMany(p => p.SicTMovimientoSalidaDetalles)
                    .HasForeignKey(d => d.AlmCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_ALMACEN");

                entity.HasOne(d => d.ItmCI)
                    .WithMany(p => p.SicTMovimientoSalidaDetalles)
                    .HasForeignKey(d => d.ItmCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_ITEM");

                entity.HasOne(d => d.MvsCI)
                    .WithMany(p => p.SicTMovimientoSalidaDetalles)
                    .HasForeignKey(d => d.MvsCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_MOVIMIENTO_SALIDA");
            });

            modelBuilder.Entity<SicTMovimientoSalidum>(entity =>
            {
                entity.HasKey(e => e.MvsCIid);

                entity.ToTable("SIC_T_MOVIMIENTO_SALIDA", "Almacen");

                entity.Property(e => e.MvsCIid).HasColumnName("mvs_c_iid");

                entity.Property(e => e.CliCVdocId)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.MovEstadoIid).HasColumnName("mov_estado_iid");

                entity.Property(e => e.MvsCBactivo).HasColumnName("mvs_c_bactivo");

                entity.Property(e => e.MvsCBingresado).HasColumnName("mvs_c_bingresado");

                entity.Property(e => e.MvsCItiposalida).HasColumnName("mvs_c_itiposalida");

                entity.Property(e => e.MvsCVdestiposalida)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mvs_c_vdestiposalida");

                entity.Property(e => e.MvsCVobservacion)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("mvs_c_vobservacion");

                entity.Property(e => e.MvsCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("mvs_c_zfecharegistro");

                entity.Property(e => e.VenCIid).HasColumnName("ven_c_iid");

                entity.HasOne(d => d.CliCVdoc)
                    .WithMany(p => p.SicTMovimientoSalida)
                    .HasForeignKey(d => d.CliCVdocId)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_CLIENTE");

                entity.HasOne(d => d.MovEstadoI)
                    .WithMany(p => p.SicTMovimientoSalida)
                    .HasForeignKey(d => d.MovEstadoIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_MOV_ESTADO");

                entity.HasOne(d => d.VenCI)
                    .WithMany(p => p.SicTMovimientoSalida)
                    .HasForeignKey(d => d.VenCIid)
                    .HasConstraintName("FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_VENTA");
            });

            modelBuilder.Entity<SicTNombCom>(entity =>
            {
                entity.HasKey(e => e.NombComCIid);

                entity.ToTable("SIC_T_NOMB_COM", "Mantenimiento");

                entity.Property(e => e.NombComCIid).HasColumnName("nomb_com_c_iid");

                entity.Property(e => e.NombComCVnomb)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nomb_com_c_vnomb");
            });

            modelBuilder.Entity<SicTOdcClase>(entity =>
            {
                entity.HasKey(e => e.OdcClaIid);

                entity.ToTable("SIC_T_ODC_CLASE", "Mantenimiento");

                entity.Property(e => e.OdcClaIid).HasColumnName("odc_cla_iid");

                entity.Property(e => e.OdcClaVdes)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("odc_cla_vdes");
            });

            modelBuilder.Entity<SicTOdcEstado>(entity =>
            {
                entity.HasKey(e => e.OdcEstadoIid);

                entity.ToTable("SIC_T_ODC_ESTADO", "Mantenimiento");

                entity.Property(e => e.OdcEstadoIid).HasColumnName("odc_estado_iid");

                entity.Property(e => e.OdcEstadoVdescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("odc_estado_vdescripcion");
            });

            modelBuilder.Entity<SicTOpcion>(entity =>
            {
                entity.HasKey(e => e.OpcCIid);

                entity.ToTable("SIC_T_OPCION", "Seguridad");

                entity.Property(e => e.OpcCIid)
                    .ValueGeneratedNever()
                    .HasColumnName("opc_c_iid");

                entity.Property(e => e.OpcCBestado).HasColumnName("opc_c_bestado");

                entity.Property(e => e.OpcCVdesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("opc_c_vdesc");
            });

            modelBuilder.Entity<SicTOrdenDeCompra>(entity =>
            {
                entity.HasKey(e => e.OdcCIid);

                entity.ToTable("SIC_T_ORDEN_DE_COMPRA", "Mantenimiento");

                entity.Property(e => e.OdcCIid).HasColumnName("odc_c_iid");

                entity.Property(e => e.EmpDirCIid).HasColumnName("emp_dir_c_iid");

                entity.Property(e => e.OdcCBactivo).HasColumnName("odc_c_bactivo");

                entity.Property(e => e.OdcCBpercepcion).HasColumnName("odc_c_bpercepcion");

                entity.Property(e => e.OdcCClaseDes)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("odc_c_clase_des");

                entity.Property(e => e.OdcCClaseIid).HasColumnName("odc_c_clase_iid");

                entity.Property(e => e.OdcCCserie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_cserie")
                    .IsFixedLength(true);

                entity.Property(e => e.OdcCEigv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_eigv");

                entity.Property(e => e.OdcCEigvcal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_eigvcal");

                entity.Property(e => e.OdcCEpercepcion)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_epercepcion");

                entity.Property(e => e.OdcCEpercepcioncal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_epercepcioncal");

                entity.Property(e => e.OdcCEsubtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_esubtotal");

                entity.Property(e => e.OdcCEtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("odc_c_etotal");

                entity.Property(e => e.OdcCIestado).HasColumnName("odc_c_iestado");

                entity.Property(e => e.OdcCIidUsuarioCreador)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_iid_usuario_creador");

                entity.Property(e => e.OdcCIidUsuarioMod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_iid_usuario_mod");

                entity.Property(e => e.OdcCVcodigo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_vcodigo");

                entity.Property(e => e.OdcCVdescestado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_vdescestado");

                entity.Property(e => e.OdcCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_vdescmoneda");

                entity.Property(e => e.OdcCVobservacion)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("odc_c_vobservacion");

                entity.Property(e => e.OdcCYmoneda).HasColumnName("odc_c_ymoneda");

                entity.Property(e => e.OdcCZfechaemi)
                    .HasColumnType("date")
                    .HasColumnName("odc_c_zfechaemi");

                entity.Property(e => e.OdcCZfechaentregaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("odc_c_zfechaentrega_fin");

                entity.Property(e => e.OdcCZfechaentregaIni)
                    .HasColumnType("datetime")
                    .HasColumnName("odc_c_zfechaentrega_ini");

                entity.Property(e => e.OdcCZfecharegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("odc_c_zfecharegistro");

                entity.Property(e => e.OdcCZfecharmod)
                    .HasColumnType("datetime")
                    .HasColumnName("odc_c_zfecharmod");

                entity.Property(e => e.ProvCVdocId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("prov_c_vdoc_id");

                entity.HasOne(d => d.EmpDirCI)
                    .WithMany(p => p.SicTOrdenDeCompras)
                    .HasForeignKey(d => d.EmpDirCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_EMP_DIRECCION");

                entity.HasOne(d => d.OdcCClaseI)
                    .WithMany(p => p.SicTOrdenDeCompras)
                    .HasForeignKey(d => d.OdcCClaseIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_ODC_CLASE");

                entity.HasOne(d => d.OdcCIestadoNavigation)
                    .WithMany(p => p.SicTOrdenDeCompras)
                    .HasForeignKey(d => d.OdcCIestado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_ODC_ESTADO");

                entity.HasOne(d => d.ProvCVdoc)
                    .WithMany(p => p.SicTOrdenDeCompras)
                    .HasForeignKey(d => d.ProvCVdocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_CLIENTE");
            });

            modelBuilder.Entity<SicTOrdenDeCompraDet>(entity =>
            {
                entity.HasKey(e => e.OdcDetCIid)
                    .HasName("PK_SIC_T_ORDEN_DE_COMPRA_DET_1");

                entity.ToTable("SIC_T_ORDEN_DE_COMPRA_DET", "Mantenimiento");

                entity.Property(e => e.OdcDetCIid).HasColumnName("odc_det_c_iid");

                entity.Property(e => e.OdcCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("odc_c_ecantidad");

                entity.Property(e => e.OdcCEpreciototal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("odc_c_epreciototal");

                entity.Property(e => e.OdcCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("odc_c_epreciounit");

                entity.Property(e => e.OdcCIid).HasColumnName("odc_c_iid");

                entity.Property(e => e.OdcCIitemid).HasColumnName("odc_c_iitemid");

                entity.HasOne(d => d.OdcCI)
                    .WithMany(p => p.SicTOrdenDeCompraDets)
                    .HasForeignKey(d => d.OdcCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_DET_SIC_T_ORDEN_DE_COMPRA");

                entity.HasOne(d => d.OdcCIitem)
                    .WithMany(p => p.SicTOrdenDeCompraDets)
                    .HasForeignKey(d => d.OdcCIitemid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_ORDEN_DE_COMPRA_DET_SIC_T_ITEM");
            });

            modelBuilder.Entity<SicTParametro>(entity =>
            {
                entity.HasKey(e => e.ParCIid);

                entity.ToTable("SIC_T_PARAMETRO", "General");

                entity.Property(e => e.ParCIid).HasColumnName("par_c_iid");

                entity.Property(e => e.ParCBactivo).HasColumnName("par_c_bactivo");

                entity.Property(e => e.ParCVdesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("par_c_vdesc");
            });

            modelBuilder.Entity<SicTParametroDet>(entity =>
            {
                entity.HasKey(e => new { e.ParCIid, e.ParDetCIid });

                entity.ToTable("SIC_T_PARAMETRO_DET", "General");

                entity.Property(e => e.ParCIid).HasColumnName("par_c_iid");

                entity.Property(e => e.ParDetCIid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("par_det_c_iid");

                entity.Property(e => e.ParDetCVcampo1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_1");

                entity.Property(e => e.ParDetCVcampo2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_2");

                entity.Property(e => e.ParDetCVcampo3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_3");

                entity.Property(e => e.ParDetCVcampo4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_4");

                entity.Property(e => e.ParDetCVcampo5)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_5");

                entity.Property(e => e.ParDetCVcampoDesc1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_desc_1");

                entity.Property(e => e.ParDetCVcampoDesc2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_desc_2");

                entity.Property(e => e.ParDetCVcampoDesc3)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_desc_3");

                entity.Property(e => e.ParDetCVcampoDesc4)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_desc_4");

                entity.Property(e => e.ParDetCVcampoDesc5)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vcampo_desc_5");

                entity.Property(e => e.ParDetCVdesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vdesc");

                entity.Property(e => e.ParDetCVobs)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("par_det_c_vobs");

                entity.HasOne(d => d.ParCI)
                    .WithMany(p => p.SicTParametroDets)
                    .HasForeignKey(d => d.ParCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PARAMETRO_DET_SIC_T_PARAMETRO");
            });

            modelBuilder.Entity<SicTPerfil>(entity =>
            {
                entity.HasKey(e => e.PerfCYid);

                entity.ToTable("SIC_T_PERFIL", "Seguridad");

                entity.Property(e => e.PerfCYid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("perf_c_yid");

                entity.Property(e => e.PerfCCestado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_c_cestado")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfCVdesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("perf_c_vdesc");

                entity.Property(e => e.PerfCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("perf_c_vnomb");
            });

            modelBuilder.Entity<SicTPerfilMenu>(entity =>
            {
                entity.HasKey(e => new { e.PerfCYid, e.MenuCIid });

                entity.ToTable("SIC_T_PERFIL_MENU", "Seguridad");

                entity.Property(e => e.PerfCYid).HasColumnName("perf_c_yid");

                entity.Property(e => e.MenuCIid).HasColumnName("menu_c_iid");

                entity.Property(e => e.PerfMenuCCalta)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_calta")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfMenuCCelim)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_celim")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfMenuCCimpre)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_cimpre")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfMenuCCmod)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_cmod")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfMenuCCproc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_cproc")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.Property(e => e.PerfMenuCCvisual)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("perf_menu_c_cvisual")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MenuCI)
                    .WithMany(p => p.SicTPerfilMenus)
                    .HasForeignKey(d => d.MenuCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PERFIL_MENU_SIC_T_MENU");

                entity.HasOne(d => d.PerfCY)
                    .WithMany(p => p.SicTPerfilMenus)
                    .HasForeignKey(d => d.PerfCYid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PERFIL_MENU_SIC_T_PERFIL");
            });

            modelBuilder.Entity<SicTPerfilOpcion>(entity =>
            {
                entity.HasKey(e => new { e.PerfCYid, e.OpcCIid });

                entity.ToTable("SIC_T_PERFIL_OPCION", "Seguridad");

                entity.Property(e => e.PerfCYid).HasColumnName("perf_c_yid");

                entity.Property(e => e.OpcCIid).HasColumnName("opc_c_iid");

                entity.HasOne(d => d.OpcCI)
                    .WithMany(p => p.SicTPerfilOpcions)
                    .HasForeignKey(d => d.OpcCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PERFIL_OPCION_SIC_T_OPCION");

                entity.HasOne(d => d.PerfCY)
                    .WithMany(p => p.SicTPerfilOpcions)
                    .HasForeignKey(d => d.PerfCYid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PERFIL_OPCION_SIC_T_PERFIL");
            });

            modelBuilder.Entity<SicTProceso>(entity =>
            {
                entity.HasKey(e => e.ProcCVid);

                entity.ToTable("SIC_T_PROCESO", "Confeccion");

                entity.Property(e => e.ProcCVid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("proc_c_vid");

                entity.Property(e => e.ProcCVdescripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("proc_c_vdescripcion");
            });

            modelBuilder.Entity<SicTProductoPartidum>(entity =>
            {
                entity.HasKey(e => e.ProPartidaCIid);

                entity.ToTable("SIC_T_PRODUCTO_PARTIDA", "Mantenimiento");

                entity.Property(e => e.ProPartidaCIid).HasColumnName("pro_partida_c_iid");

                entity.Property(e => e.IsfCIid).HasColumnName("isf_c_iid");

                entity.Property(e => e.ProPartidaCVcodigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pro_partida_c_vcodigo");

                entity.Property(e => e.ProPartidaCVdescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pro_partida_c_vdescripcion");

                entity.HasOne(d => d.IsfCI)
                    .WithMany(p => p.SicTProductoPartida)
                    .HasForeignKey(d => d.IsfCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PRODUCTO_PARTIDA_ITEM_SUBFAMILIA");
            });

            modelBuilder.Entity<SicTProvincium>(entity =>
            {
                entity.HasKey(e => e.ProvCCcod);

                entity.ToTable("SIC_T_PROVINCIA", "Mantenimiento");

                entity.Property(e => e.ProvCCcod)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("prov_c_ccod")
                    .IsFixedLength(true);

                entity.Property(e => e.DepaCCcod)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("depa_c_ccod")
                    .IsFixedLength(true);

                entity.Property(e => e.ProvCVnomb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prov_c_vnomb");

                entity.HasOne(d => d.DepaCCcodNavigation)
                    .WithMany(p => p.SicTProvincia)
                    .HasForeignKey(d => d.DepaCCcod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_PROVINCIA_SIC_T_DEPARTAMENTO");
            });

            modelBuilder.Entity<SicTSegmento>(entity =>
            {
                entity.HasKey(e => e.SegmentoCYid);

                entity.ToTable("SIC_T_SEGMENTO", "Mantenimiento");

                entity.Property(e => e.SegmentoCYid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("segmento_c_yid");

                entity.Property(e => e.SegmentoCVcodigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("segmento_c_vcodigo");

                entity.Property(e => e.SegmentoCVdescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("segmento_c_vdescripcion");
            });

            modelBuilder.Entity<SicTTalla>(entity =>
            {
                entity.HasKey(e => e.TallaCVid);

                entity.ToTable("SIC_T_TALLA", "Confeccion");

                entity.Property(e => e.TallaCVid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("talla_c_vid");

                entity.Property(e => e.TallaCVcodigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("talla_c_vcodigo");

                entity.Property(e => e.TallaCVdescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("talla_c_vdescripcion");
            });

            modelBuilder.Entity<SicTTasaCambio>(entity =>
            {
                entity.HasKey(e => e.TscCIid);

                entity.ToTable("SIC_T_TASA_CAMBIO", "Mantenimiento");

                entity.Property(e => e.TscCIid).HasColumnName("tsc_c_iid");

                entity.Property(e => e.TscCDfin)
                    .HasColumnType("datetime")
                    .HasColumnName("tsc_c_dfin");

                entity.Property(e => e.TscCDinicio)
                    .HasColumnType("datetime")
                    .HasColumnName("tsc_c_dinicio");

                entity.Property(e => e.TscCEcompra)
                    .HasColumnType("decimal(17, 4)")
                    .HasColumnName("tsc_c_ecompra");

                entity.Property(e => e.TscCEventa)
                    .HasColumnType("decimal(17, 4)")
                    .HasColumnName("tsc_c_eventa");
            });

            modelBuilder.Entity<SicTUnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.UndCYid);

                entity.ToTable("SIC_T_UNIDAD_MEDIDA", "Mantenimiento");

                entity.Property(e => e.UndCYid).HasColumnName("und_c_yid");

                entity.Property(e => e.UndCBactivo).HasColumnName("und_c_bactivo");

                entity.Property(e => e.UndCVdesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("und_c_vdesc");
            });

            modelBuilder.Entity<SicTUsuario>(entity =>
            {
                entity.HasKey(e => e.UsuaCCdocId);

                entity.ToTable("SIC_T_USUARIO", "Seguridad");

                entity.Property(e => e.UsuaCCdocId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cdoc_id");

                entity.Property(e => e.UsuaCBestado).HasColumnName("usua_c_bestado");

                entity.Property(e => e.UsuaCBpropietarioadministrador).HasColumnName("usua_c_bpropietarioadministrador");

                entity.Property(e => e.UsuaCCapeMat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cape_mat");

                entity.Property(e => e.UsuaCCapeNombres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cape_nombres");

                entity.Property(e => e.UsuaCCapePat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cape_pat");

                entity.Property(e => e.UsuaCCidempresa)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cidempresa")
                    .IsFixedLength(true);

                entity.Property(e => e.UsuaCCusuRed)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cusu_red")
                    .IsFixedLength(true);

                entity.Property(e => e.UsuaCVpass)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_vpass");
            });

            modelBuilder.Entity<SicTUsuarioOpcion>(entity =>
            {
                entity.HasKey(e => new { e.UsuaCCdocId, e.OpcCIid });

                entity.ToTable("SIC_T_USUARIO_OPCION", "Seguridad");

                entity.Property(e => e.UsuaCCdocId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cdoc_id");

                entity.Property(e => e.OpcCIid).HasColumnName("opc_c_iid");

                entity.HasOne(d => d.OpcCI)
                    .WithMany(p => p.SicTUsuarioOpcions)
                    .HasForeignKey(d => d.OpcCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_USUARIO_OPCION_SIC_T_OPCION");

                entity.HasOne(d => d.UsuaCCdoc)
                    .WithMany(p => p.SicTUsuarioOpcions)
                    .HasForeignKey(d => d.UsuaCCdocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_USUARIO_OPCION_SIC_T_USUARIO");
            });

            modelBuilder.Entity<SicTUsuarioPerfil>(entity =>
            {
                entity.HasKey(e => new { e.PerfCYid, e.UsuaCCdocId })
                    .HasName("PK__SIC_T_US__003231E46900EB32");

                entity.ToTable("SIC_T_USUARIO_PERFIL", "Seguridad");

                entity.Property(e => e.PerfCYid).HasColumnName("perf_c_yid");

                entity.Property(e => e.UsuaCCdocId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("usua_c_cdoc_id");

                entity.Property(e => e.UsuaPerfilCCestado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("usua_perfil_c_cestado")
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SicTVenEstado>(entity =>
            {
                entity.HasKey(e => e.VenEstCIid);

                entity.ToTable("SIC_T_VEN_ESTADO", "Mantenimiento");

                entity.Property(e => e.VenEstCIid).HasColumnName("ven_est_c_iid");

                entity.Property(e => e.VenEstCVdescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ven_est_c_vdescripcion");
            });

            modelBuilder.Entity<SicTVentaDetalle>(entity =>
            {
                entity.HasKey(e => e.VenDetCIid);

                entity.ToTable("SIC_T_VENTA_DETALLE", "Mantenimiento");

                entity.Property(e => e.VenDetCIid).HasColumnName("ven_det_c_iid");

                entity.Property(e => e.VenCIid).HasColumnName("ven_c_iid");

                entity.Property(e => e.VenDetCEcantidad)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("ven_det_c_ecantidad");

                entity.Property(e => e.VenDetCEpreciototal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("ven_det_c_epreciototal");

                entity.Property(e => e.VenDetCEpreciounit)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("ven_det_c_epreciounit");

                entity.Property(e => e.VenDetCIidalmacen).HasColumnName("ven_det_c_iidalmacen");

                entity.Property(e => e.VenDetCIitemid).HasColumnName("ven_det_c_iitemid");

                entity.HasOne(d => d.VenCI)
                    .WithMany(p => p.SicTVentaDetalles)
                    .HasForeignKey(d => d.VenCIid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_DETALLE_SIC_T_VENTA");

                entity.HasOne(d => d.VenDetCIidalmacenNavigation)
                    .WithMany(p => p.SicTVentaDetalles)
                    .HasForeignKey(d => d.VenDetCIidalmacen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_DETALLE_SIC_T_ALMACEN");

                entity.HasOne(d => d.VenDetCIitem)
                    .WithMany(p => p.SicTVentaDetalles)
                    .HasForeignKey(d => d.VenDetCIitemid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_DETALLE_SIC_T_ITEM");
            });

            modelBuilder.Entity<SicTVentum>(entity =>
            {
                entity.HasKey(e => e.VenCIid);

                entity.ToTable("SIC_T_VENTA", "Mantenimiento");

                entity.Property(e => e.VenCIid).HasColumnName("ven_c_iid");

                entity.Property(e => e.VenCBactivo).HasColumnName("ven_c_bactivo");

                entity.Property(e => e.VenCEigv)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ven_c_eigv");

                entity.Property(e => e.VenCEigvcal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ven_c_eigvcal");

                entity.Property(e => e.VenCEsubtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ven_c_esubtotal");

                entity.Property(e => e.VenCEtotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ven_c_etotal");

                entity.Property(e => e.VenCIcentrocosto).HasColumnName("ven_c_icentrocosto");

                entity.Property(e => e.VenCIestado).HasColumnName("ven_c_iestado");

                entity.Property(e => e.VenCItipodoc).HasColumnName("ven_c_itipodoc");

                entity.Property(e => e.VenCVdescmoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ven_c_vdescmoneda");

                entity.Property(e => e.VenCVdestipodoc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ven_c_vdestipodoc");

                entity.Property(e => e.VenCVdoccliId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("ven_c_vdoccli_id");

                entity.Property(e => e.VenCVestado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ven_c_vestado");

                entity.Property(e => e.VenCYmoneda).HasColumnName("ven_c_ymoneda");

                entity.Property(e => e.VenCZfecha)
                    .HasColumnType("datetime")
                    .HasColumnName("ven_c_zfecha");

                entity.HasOne(d => d.VenCIcentrocostoNavigation)
                    .WithMany(p => p.SicTVenta)
                    .HasForeignKey(d => d.VenCIcentrocosto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_SIC_T_EMP_CENTRO_COSTO");

                entity.HasOne(d => d.VenCIestadoNavigation)
                    .WithMany(p => p.SicTVenta)
                    .HasForeignKey(d => d.VenCIestado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_SIC_T_VEN_ESTADO");

                entity.HasOne(d => d.VenCVdoccli)
                    .WithMany(p => p.SicTVenta)
                    .HasForeignKey(d => d.VenCVdoccliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIC_T_VENTA_SIC_T_CLIENTE");
            });

            modelBuilder.Entity<SicTZonaReparto>(entity =>
            {
                entity.HasKey(e => e.ZonaRepCYid);

                entity.ToTable("SIC_T_ZONA_REPARTO", "Mantenimiento");

                entity.Property(e => e.ZonaRepCYid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("zona_rep_c_yid");

                entity.Property(e => e.ZonaRepCCzona)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("zona_rep_c_czona")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SicTZonaRepartoLugar>(entity =>
            {
                entity.HasKey(e => e.ZonaRepLugCIid);

                entity.ToTable("SIC_T_ZONA_REPARTO_LUGAR", "Mantenimiento");

                entity.Property(e => e.ZonaRepLugCIid).HasColumnName("zona_rep_lug_c_iid");

                entity.Property(e => e.ZonaRepCYid).HasColumnName("zona_rep_c_yid");

                entity.Property(e => e.ZonaRepLugCVdesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("zona_rep_lug_c_vdesc");

                entity.HasOne(d => d.ZonaRepCY)
                    .WithMany(p => p.SicTZonaRepartoLugars)
                    .HasForeignKey(d => d.ZonaRepCYid)
                    .HasConstraintName("FK_SIC_T_ZONA_REPARTO_LUGAR_SIC_T_ZONA_REPARTO");
            });

            modelBuilder.Entity<SicVwClienteListum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SIC_VW_CLIENTE_LISTA", "Mantenimiento");

                entity.Property(e => e.CliCVdocId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vdoc_id");

                entity.Property(e => e.CliCVrazSoc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_c_vraz_soc");

                entity.Property(e => e.NombComCIid).HasColumnName("nomb_com_c_iid");

                entity.Property(e => e.NombComCVnomb)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nomb_com_c_vnomb");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
