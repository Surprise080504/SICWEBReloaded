using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SICWEB.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Mantenimiento");

            migrationBuilder.EnsureSchema(
                name: "Facturacion");

            migrationBuilder.EnsureSchema(
                name: "Venta");

            migrationBuilder.EnsureSchema(
                name: "Confeccion");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.EnsureSchema(
                name: "Almacen");

            migrationBuilder.CreateTable(
                name: "SIC_T_ALMACEN",
                schema: "Mantenimiento",
                columns: table => new
                {
                    alm_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alm_c_bactivo = table.Column<bool>(type: "bit", nullable: true),
                    alm_c_vdesc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ALMACEN", x => x.alm_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CATEGORIA",
                schema: "Confeccion",
                columns: table => new
                {
                    cate_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    cate_c_vcodigo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    cate_c_vdescripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CATEGORIA", x => x.cate_c_vid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_CONTAC_CARGO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_contac_cargo_c_yid = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cli_contac_cargo_c_vnomb = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_CONTAC_CARGO", x => x.cli_contac_cargo_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_RS_HISTORICO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_rs_h_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    cli_rs_h_c_vraz_soc = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    cli_rs_h_c_dfec_reg = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_RS_HISTORICO", x => x.cli_rs_h_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_SCORING",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_scor_c_cletra = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    cli_scor_c_vobserv = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_SCORING", x => x.cli_scor_c_cletra);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_COLAB_AREA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    colab_area_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    colab_area_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_COLAB_AREA", x => x.colab_area_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_COLAB_CARGO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    colab_cargo_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    colab_cargo_c_vnomb = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_COLAB_CARGO", x => x.colab_cargo_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_COLOR",
                schema: "Confeccion",
                columns: table => new
                {
                    color_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    color_c_vcodigo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    color_c_vdescripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_COLOR", x => x.color_c_vid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CONCEPTO",
                schema: "General",
                columns: table => new
                {
                    con_c_iid = table.Column<int>(type: "int", nullable: false),
                    con_c_vdes = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CONCEPTO", x => x.con_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_DEPARTAMENTO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    depa_c_ccod = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    depa_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_DEPARTAMENTO", x => x.depa_c_ccod);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_EMP_CENTRO_COSTO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    emp_cst_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_cst_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    emp_cst_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    emp_cst_c_vseriefactura = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    emp_cst_c_inumerofact = table.Column<int>(type: "int", nullable: false),
                    emp_cst_c_vserieboleta = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    emp_cst_c_inumeroboleta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_EMP_CENTRO_COSTO_1", x => x.emp_cst_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_EMPRESA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    emp_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_c_vruc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    emp_c_vrazonsocial = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_EMPRESA", x => x.emp_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_IGV",
                schema: "Mantenimiento",
                columns: table => new
                {
                    igv_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    igv_c_eigv = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    igv_c_dinicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    igv_c_dfin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_IGV", x => x.igv_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_IMPRESORA",
                schema: "General",
                columns: table => new
                {
                    imp_c_iid = table.Column<int>(type: "int", nullable: false),
                    imp_c_vruta = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_IMPRESORA", x => x.imp_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MENU",
                schema: "Seguridad",
                columns: table => new
                {
                    menu_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menu_c_iid_padre = table.Column<int>(type: "int", nullable: true),
                    menu_c_vnomb = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    menu_c_ynivel = table.Column<byte>(type: "tinyint", nullable: true),
                    menu_c_vpag_asp = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MENU", x => x.menu_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_MENU_SIC_T_MENU",
                        column: x => x.menu_c_iid_padre,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_MENU",
                        principalColumn: "menu_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MOV_ESTADO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    mov_estado_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mov_estado_vdescrpcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MOV_ESTADO", x => x.mov_estado_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_NOMB_COM",
                schema: "Mantenimiento",
                columns: table => new
                {
                    nomb_com_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomb_com_c_vnomb = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_NOMB_COM", x => x.nomb_com_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ODC_CLASE",
                schema: "Mantenimiento",
                columns: table => new
                {
                    odc_cla_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    odc_cla_vdes = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ODC_CLASE", x => x.odc_cla_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ODC_ESTADO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    odc_estado_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    odc_estado_vdescripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ODC_ESTADO", x => x.odc_estado_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_OPCION",
                schema: "Seguridad",
                columns: table => new
                {
                    opc_c_iid = table.Column<int>(type: "int", nullable: false),
                    opc_c_vdesc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    opc_c_bestado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_OPCION", x => x.opc_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PARAMETRO",
                schema: "General",
                columns: table => new
                {
                    par_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    par_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    par_c_bactivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PARAMETRO", x => x.par_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PERFIL",
                schema: "Seguridad",
                columns: table => new
                {
                    perf_c_yid = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perf_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    perf_c_vdesc = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    perf_c_cestado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PERFIL", x => x.perf_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PROCESO",
                schema: "Confeccion",
                columns: table => new
                {
                    proc_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    proc_c_vdescripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PROCESO", x => x.proc_c_vid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_SEGMENTO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    segmento_c_yid = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    segmento_c_vcodigo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    segmento_c_vdescripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_SEGMENTO", x => x.segmento_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_TALLA",
                schema: "Confeccion",
                columns: table => new
                {
                    talla_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    talla_c_vcodigo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    talla_c_vdescripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_TALLA", x => x.talla_c_vid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_TASA_CAMBIO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    tsc_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tsc_c_ecompra = table.Column<decimal>(type: "decimal(17,4)", nullable: false),
                    tsc_c_eventa = table.Column<decimal>(type: "decimal(17,4)", nullable: false),
                    tsc_c_dinicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    tsc_c_dfin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_TASA_CAMBIO", x => x.tsc_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_UNIDAD_MEDIDA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    und_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    und_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    und_c_bactivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_UNIDAD_MEDIDA", x => x.und_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_USUARIO",
                schema: "Seguridad",
                columns: table => new
                {
                    usua_c_cdoc_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    usua_c_cusu_red = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    usua_c_bpropietarioadministrador = table.Column<bool>(type: "bit", nullable: true),
                    usua_c_cidempresa = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    usua_c_cape_pat = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    usua_c_cape_mat = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    usua_c_cape_nombres = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    usua_c_vpass = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    usua_c_bestado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_USUARIO", x => x.usua_c_cdoc_id);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_USUARIO_PERFIL",
                schema: "Seguridad",
                columns: table => new
                {
                    perf_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    usua_c_cdoc_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    usua_perfil_c_cestado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SIC_T_US__003231E46900EB32", x => new { x.perf_c_yid, x.usua_c_cdoc_id });
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_VEN_ESTADO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    ven_est_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ven_est_c_vdescripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_VEN_ESTADO", x => x.ven_est_c_iid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ZONA_REPARTO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    zona_rep_c_yid = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zona_rep_c_czona = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ZONA_REPARTO", x => x.zona_rep_c_yid);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_COLABORADOR",
                schema: "Mantenimiento",
                columns: table => new
                {
                    colab_c_cdoc_id = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    colab_c_cusu_red = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    colab_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colab_c_vape_pat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colab_c_vape_mat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    colab_area_c_yid = table.Column<byte>(type: "tinyint", nullable: true),
                    colab_cargo_c_yid = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_COLABORADOR", x => x.colab_c_cdoc_id);
                    table.ForeignKey(
                        name: "FK_SIC_T_COLABORADOR_SIC_T_COLAB_AREA",
                        column: x => x.colab_area_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_COLAB_AREA",
                        principalColumn: "colab_area_c_yid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_COLABORADOR_SIC_T_COLAB_CARGO",
                        column: x => x.colab_cargo_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_COLAB_CARGO",
                        principalColumn: "colab_cargo_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PROVINCIA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    prov_c_ccod = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    prov_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    depa_c_ccod = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PROVINCIA", x => x.prov_c_ccod);
                    table.ForeignKey(
                        name: "FK_SIC_T_PROVINCIA_SIC_T_DEPARTAMENTO",
                        column: x => x.depa_c_ccod,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_DEPARTAMENTO",
                        principalColumn: "depa_c_ccod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ALMACEN_CENTRO_COSTO",
                columns: table => new
                {
                    alm_cst_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alm_cst_c_iid_centro_costo = table.Column<int>(type: "int", nullable: false),
                    alm_cst_c_iid_almacen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ALMACEN_CENTRO_COSTO", x => x.alm_cst_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ALMACEN_CENTRO_COSTO_SIC_T_ALMACEN",
                        column: x => x.alm_cst_c_iid_almacen,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ALMACEN",
                        principalColumn: "alm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ALMACEN_CENTRO_COSTO_SIC_T_EMP_CENTRO_COSTO",
                        column: x => x.alm_cst_c_iid_centro_costo,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_EMP_CENTRO_COSTO",
                        principalColumn: "emp_cst_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_EMP_DIRECCION",
                schema: "Mantenimiento",
                columns: table => new
                {
                    emp_dir_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_dir_c_vdireccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    emp_dir_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    emp_dir_c_iid_centrocosto = table.Column<int>(type: "int", nullable: false),
                    emp_dir_c_itipodirec = table.Column<int>(type: "int", nullable: false),
                    emp_dir_c_ccod_ubig = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    emp_dir_c_vtipodirec = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_EMP_DIRECCION", x => x.emp_dir_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_EMP_DIRECCION_SIC_T_EMP_CENTRO_COSTO",
                        column: x => x.emp_dir_c_iid_centrocosto,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_EMP_CENTRO_COSTO",
                        principalColumn: "emp_cst_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PARAMETRO_DET",
                schema: "General",
                columns: table => new
                {
                    par_c_iid = table.Column<int>(type: "int", nullable: false),
                    par_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    par_det_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    par_det_c_vcampo_1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    par_det_c_vcampo_desc_1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    par_det_c_vcampo_2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    par_det_c_vcampo_desc_2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    par_det_c_vcampo_3 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    par_det_c_vcampo_desc_3 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    par_det_c_vcampo_4 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    par_det_c_vcampo_desc_4 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    par_det_c_vcampo_5 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    par_det_c_vcampo_desc_5 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    par_det_c_vobs = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PARAMETRO_DET", x => new { x.par_c_iid, x.par_det_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_PARAMETRO_DET_SIC_T_PARAMETRO",
                        column: x => x.par_c_iid,
                        principalSchema: "General",
                        principalTable: "SIC_T_PARAMETRO",
                        principalColumn: "par_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PERFIL_MENU",
                schema: "Seguridad",
                columns: table => new
                {
                    perf_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    menu_c_iid = table.Column<int>(type: "int", nullable: false),
                    perf_menu_c_calta = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')"),
                    perf_menu_c_cmod = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')"),
                    perf_menu_c_celim = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')"),
                    perf_menu_c_cvisual = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')"),
                    perf_menu_c_cimpre = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')"),
                    perf_menu_c_cproc = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('A')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PERFIL_MENU", x => new { x.perf_c_yid, x.menu_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_PERFIL_MENU_SIC_T_MENU",
                        column: x => x.menu_c_iid,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_MENU",
                        principalColumn: "menu_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_PERFIL_MENU_SIC_T_PERFIL",
                        column: x => x.perf_c_yid,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_PERFIL",
                        principalColumn: "perf_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PERFIL_OPCION",
                schema: "Seguridad",
                columns: table => new
                {
                    perf_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    opc_c_iid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PERFIL_OPCION", x => new { x.perf_c_yid, x.opc_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_PERFIL_OPCION_SIC_T_OPCION",
                        column: x => x.opc_c_iid,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_OPCION",
                        principalColumn: "opc_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_PERFIL_OPCION_SIC_T_PERFIL",
                        column: x => x.perf_c_yid,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_PERFIL",
                        principalColumn: "perf_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ITEM_FAMILIA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    ifm_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ifm_c_des = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ifm_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    segmento_c_yid = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_FAMILIA_ITEM", x => x.ifm_c_iid);
                    table.ForeignKey(
                        name: "FK_ITEM_FAMILIA_SEGMENTO",
                        column: x => x.segmento_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_SEGMENTO",
                        principalColumn: "segmento_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_USUARIO_OPCION",
                schema: "Seguridad",
                columns: table => new
                {
                    usua_c_cdoc_id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    opc_c_iid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_USUARIO_OPCION", x => new { x.usua_c_cdoc_id, x.opc_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_USUARIO_OPCION_SIC_T_OPCION",
                        column: x => x.opc_c_iid,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_OPCION",
                        principalColumn: "opc_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_USUARIO_OPCION_SIC_T_USUARIO",
                        column: x => x.usua_c_cdoc_id,
                        principalSchema: "Seguridad",
                        principalTable: "SIC_T_USUARIO",
                        principalColumn: "usua_c_cdoc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ZONA_REPARTO_LUGAR",
                schema: "Mantenimiento",
                columns: table => new
                {
                    zona_rep_lug_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zona_rep_c_yid = table.Column<byte>(type: "tinyint", nullable: true),
                    zona_rep_lug_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ZONA_REPARTO_LUGAR", x => x.zona_rep_lug_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ZONA_REPARTO_LUGAR_SIC_T_ZONA_REPARTO",
                        column: x => x.zona_rep_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ZONA_REPARTO",
                        principalColumn: "zona_rep_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLIENTE",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    cli_c_vraz_soc = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    cli_c_vpartida = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    cli_c_vrubro = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    cli_c_ctlf = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    cli_c_dfec_aniv = table.Column<DateTime>(type: "date", nullable: true),
                    cli_c_btipo_pers = table.Column<bool>(type: "bit", nullable: true),
                    colab_c_cdoc_id = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    cli_scor_c_cletra = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    cli_c_bactivo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    zona_rep_c_yid = table.Column<byte>(type: "tinyint", nullable: true),
                    cli_c_dfecharegistra = table.Column<DateTime>(type: "datetime", nullable: false),
                    cli_c_dfechaactualiza = table.Column<DateTime>(type: "datetime", nullable: true),
                    cli_c_dfec_const = table.Column<DateTime>(type: "date", nullable: true),
                    cli_c_bproveedor = table.Column<bool>(type: "bit", nullable: true),
                    cli_c_bcliente = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLIENTE", x => x.cli_c_vdoc_id);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLIENTE_SIC_T_CLI_SCORING",
                        column: x => x.cli_scor_c_cletra,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLI_SCORING",
                        principalColumn: "cli_scor_c_cletra",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLIENTE_SIC_T_COLABORADOR",
                        column: x => x.colab_c_cdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_COLABORADOR",
                        principalColumn: "colab_c_cdoc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLIENTE_SIC_T_ZONA_REPARTO",
                        column: x => x.zona_rep_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ZONA_REPARTO",
                        principalColumn: "zona_rep_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_DISTRITO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    dist_c_ccod_ubig = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    dist_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    prov_c_ccod = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_DISTRITO", x => x.dist_c_ccod_ubig);
                    table.ForeignKey(
                        name: "FK_SIC_T_DISTRITO_SIC_T_PROVINCIA",
                        column: x => x.prov_c_ccod,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_PROVINCIA",
                        principalColumn: "prov_c_ccod",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ITEM_SUB_FAMILIA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    isf_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isf_c_vdesc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    isf_c_ifm_iid = table.Column<int>(type: "int", nullable: false),
                    isf_c_bactivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_SUB_FAMILIA_ITEM", x => x.isf_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ITEM_SUB_FAMILIA_SIC_T_ITEM_FAMILIA",
                        column: x => x.isf_c_ifm_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM_FAMILIA",
                        principalColumn: "ifm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_CONTACTO",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_contac_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cli_contac_c_cdoc_id = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true),
                    cli_contac_c_vnomb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cli_contac_c_vape_pat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cli_contac_c_vape_mat = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cli_contac_c_ctlf = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    cli_contac_c_ccel = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    cli_contac_c_vcorreo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cli_contac_c_dfec_cump = table.Column<DateTime>(type: "date", nullable: true),
                    cli_contac_cargo_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    cli_contac_c_vobserv = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cli_contac_c_bactivo = table.Column<bool>(type: "bit", nullable: true),
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_CONTACTO", x => x.cli_contac_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLI_CONTACTO_SIC_T_CLI_CONTAC_CARGO",
                        column: x => x.cli_contac_cargo_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLI_CONTAC_CARGO",
                        principalColumn: "cli_contac_cargo_c_yid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLI_CONTACTO_SIC_T_CLIENTE",
                        column: x => x.cli_c_vdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_NOMB_COM",
                schema: "Mantenimiento",
                columns: table => new
                {
                    nomb_com_c_iid = table.Column<int>(type: "int", nullable: false),
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_NOMB_COM", x => new { x.cli_c_vdoc_id, x.nomb_com_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_CLI_NOMB_COM_SIC_T_CLIENTE",
                        column: x => x.cli_c_vdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ORDEN_DE_COMPRA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    odc_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    odc_c_vcodigo = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    odc_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    odc_c_ymoneda = table.Column<byte>(type: "tinyint", nullable: false),
                    odc_c_esubtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_etotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_eigv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_eigvcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_epercepcion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_epercepcioncal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    odc_c_iestado = table.Column<int>(type: "int", nullable: false),
                    odc_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    odc_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    odc_c_vdescestado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    prov_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    odc_c_zfechaentrega_ini = table.Column<DateTime>(type: "datetime", nullable: false),
                    odc_c_zfechaentrega_fin = table.Column<DateTime>(type: "datetime", nullable: false),
                    odc_c_iid_usuario_creador = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    odc_c_iid_usuario_mod = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    odc_c_zfecharmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    odc_c_vobservacion = table.Column<string>(type: "varchar(350)", unicode: false, maxLength: 350, nullable: false),
                    odc_c_clase_iid = table.Column<int>(type: "int", nullable: false),
                    odc_c_clase_des = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    odc_c_bpercepcion = table.Column<bool>(type: "bit", nullable: false),
                    emp_dir_c_iid = table.Column<int>(type: "int", nullable: false),
                    odc_c_cserie = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    odc_c_zfechaemi = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ORDEN_DE_COMPRA", x => x.odc_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_CLIENTE",
                        column: x => x.prov_c_vdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_EMP_DIRECCION",
                        column: x => x.emp_dir_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_EMP_DIRECCION",
                        principalColumn: "emp_dir_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_ODC_CLASE",
                        column: x => x.odc_c_clase_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ODC_CLASE",
                        principalColumn: "odc_cla_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_SIC_T_ODC_ESTADO",
                        column: x => x.odc_c_iestado,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ODC_ESTADO",
                        principalColumn: "odc_estado_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_VENTA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    ven_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ven_c_zfecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    ven_c_ymoneda = table.Column<byte>(type: "tinyint", nullable: false),
                    ven_c_esubtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ven_c_etotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ven_c_vdoccli_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    ven_c_eigv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ven_c_eigvcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ven_c_itipodoc = table.Column<int>(type: "int", nullable: false),
                    ven_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ven_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    ven_c_vdestipodoc = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ven_c_icentrocosto = table.Column<int>(type: "int", nullable: false),
                    ven_c_iestado = table.Column<int>(type: "int", nullable: false),
                    ven_c_vestado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_VENTA", x => x.ven_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_SIC_T_CLIENTE",
                        column: x => x.ven_c_vdoccli_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_SIC_T_EMP_CENTRO_COSTO",
                        column: x => x.ven_c_icentrocosto,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_EMP_CENTRO_COSTO",
                        principalColumn: "emp_cst_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_SIC_T_VEN_ESTADO",
                        column: x => x.ven_c_iestado,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VEN_ESTADO",
                        principalColumn: "ven_est_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_CLI_DIRECCION",
                schema: "Mantenimiento",
                columns: table => new
                {
                    cli_direc_c_iid = table.Column<int>(type: "int", nullable: false),
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    cli_direc_c_vdirec = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    cli_direc_c_ctipo = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    dist_c_ccod_ubig = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: true),
                    cli_direc_c_bactivo = table.Column<bool>(type: "bit", nullable: true),
                    cli_direc_c_czonarep = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_CLI_DIRECCION", x => new { x.cli_c_vdoc_id, x.cli_direc_c_iid });
                    table.ForeignKey(
                        name: "FK_SIC_T_CLI_DIRECCION_SIC_T_CLIENTE",
                        column: x => x.cli_c_vdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_CLI_DIRECCION_SIC_T_DISTRITO",
                        column: x => x.dist_c_ccod_ubig,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_DISTRITO",
                        principalColumn: "dist_c_ccod_ubig",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_PRODUCTO_PARTIDA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    pro_partida_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pro_partida_c_vcodigo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    pro_partida_c_vdescripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    isf_c_iid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_PRODUCTO_PARTIDA", x => x.pro_partida_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_PRODUCTO_PARTIDA_ITEM_SUBFAMILIA",
                        column: x => x.isf_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM_SUB_FAMILIA",
                        principalColumn: "isf_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MOVIMIENTO_ENTRADA",
                schema: "Mantenimiento",
                columns: table => new
                {
                    mve_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    odc_c_iid = table.Column<int>(type: "int", nullable: false),
                    mve_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    mve_c_zguiafecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    mve_c_zfacturafecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    mve_c_vguiacodigo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    mve_c_vfacturacodigo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    mve_c_iidalmacen = table.Column<int>(type: "int", nullable: false),
                    mve_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    mve_c_iestado = table.Column<int>(type: "int", nullable: false),
                    mve_c_vdesestado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mve_c_vobservacion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    mve_c_bingresado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MOVIMIENTO_ENTRADA", x => x.mve_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_ALMACEN",
                        column: x => x.mve_c_iidalmacen,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ALMACEN",
                        principalColumn: "alm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_MOV_ESTADO",
                        column: x => x.mve_c_iestado,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_MOV_ESTADO",
                        principalColumn: "mov_estado_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_ENTRADA_SIC_T_ORDEN_DE_COMPRA",
                        column: x => x.odc_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ORDEN_DE_COMPRA",
                        principalColumn: "odc_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_BOLETA",
                schema: "Facturacion",
                columns: table => new
                {
                    bol_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bol_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    bol_c_serie = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    bol_c_numero = table.Column<int>(type: "int", nullable: false),
                    bol_c_iventa = table.Column<int>(type: "int", nullable: false),
                    bol_c_eigv = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_eigvcal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_esubtotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_etotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_imoneda = table.Column<int>(type: "int", nullable: false),
                    bol_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    bol_c_bimpreso = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_BOLETA", x => x.bol_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_BOLETA_SIC_T_VENTA",
                        column: x => x.bol_c_iventa,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_BOLETA",
                schema: "Venta",
                columns: table => new
                {
                    bol_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bol_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    bol_c_serie = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    bol_c_numero = table.Column<int>(type: "int", nullable: false),
                    bol_c_iventa = table.Column<int>(type: "int", nullable: false),
                    bol_c_eigv = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_eigvcal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_esubtotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_etotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_c_imoneda = table.Column<int>(type: "int", nullable: false),
                    bol_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_BOLETA", x => x.bol_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_BOLETA_SIC_T_VENTA",
                        column: x => x.bol_c_iventa,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_FACTURA",
                schema: "Facturacion",
                columns: table => new
                {
                    fac_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fac_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    fac_c_serie = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    fac_c_numero = table.Column<int>(type: "int", nullable: false),
                    fac_c_iventa = table.Column<int>(type: "int", nullable: false),
                    fac_c_eigv = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_eigvcal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_esubtotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_etotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_imoneda = table.Column<int>(type: "int", nullable: false),
                    fac_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    fac_c_bimpreso = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_FACTURA", x => x.fac_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_FACTURA_SIC_T_VENTA",
                        column: x => x.fac_c_iventa,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_FACTURA",
                schema: "Venta",
                columns: table => new
                {
                    fac_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fac_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    fac_c_serie = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    fac_c_numero = table.Column<int>(type: "int", nullable: false),
                    fac_c_iventa = table.Column<int>(type: "int", nullable: false),
                    fac_c_eigv = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_eigvcal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_esubtotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_etotal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_c_imoneda = table.Column<int>(type: "int", nullable: false),
                    fac_c_vdescmoneda = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_FACTURA", x => x.fac_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_FACTURA_SIC_T_VENTA",
                        column: x => x.fac_c_iventa,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MOVIMIENTO_SALIDA",
                schema: "Almacen",
                columns: table => new
                {
                    mvs_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mvs_c_itiposalida = table.Column<int>(type: "int", nullable: false),
                    mvs_c_vdestiposalida = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    mvs_c_zfecharegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    mvs_c_bingresado = table.Column<bool>(type: "bit", nullable: false),
                    ven_c_iid = table.Column<int>(type: "int", nullable: true),
                    mvs_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    cli_c_vdoc_id = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    mov_estado_iid = table.Column<int>(type: "int", nullable: false),
                    mvs_c_vobservacion = table.Column<string>(type: "varchar(350)", unicode: false, maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MOVIMIENTO_SALIDA", x => x.mvs_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_CLIENTE",
                        column: x => x.cli_c_vdoc_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_CLIENTE",
                        principalColumn: "cli_c_vdoc_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_MOV_ESTADO",
                        column: x => x.mov_estado_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_MOV_ESTADO",
                        principalColumn: "mov_estado_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_SIC_T_VENTA",
                        column: x => x.ven_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ITEM",
                schema: "Mantenimiento",
                columns: table => new
                {
                    itm_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itm_c_ccodigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    itm_c_vdescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    itm_c_dprecio_compra = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    und_c_yid = table.Column<byte>(type: "tinyint", nullable: false),
                    itm_c_bactivo = table.Column<bool>(type: "bit", nullable: false),
                    itm_c_dprecio_venta = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    pro_partida_c_iid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ITEM", x => x.itm_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ITEM_PRODUCTO_PARTIDA",
                        column: x => x.pro_partida_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_PRODUCTO_PARTIDA",
                        principalColumn: "pro_partida_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ITEM_SIC_T_UNIDAD_MEDIDA",
                        column: x => x.und_c_yid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_UNIDAD_MEDIDA",
                        principalColumn: "und_c_yid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_BOLETA_DETALLE",
                schema: "Venta",
                columns: table => new
                {
                    bol_det_c_iid = table.Column<int>(type: "int", nullable: false),
                    bol_c_iid = table.Column<int>(type: "int", nullable: false),
                    bol_det_c_iitem = table.Column<int>(type: "int", nullable: false),
                    bol_det_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_det_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_BOLETA_DETALLE", x => x.bol_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_BOLETA_DETALLE_SIC_T_BOLETA",
                        column: x => x.bol_c_iid,
                        principalSchema: "Venta",
                        principalTable: "SIC_T_BOLETA",
                        principalColumn: "bol_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_FACTURA_DETALLE",
                schema: "Venta",
                columns: table => new
                {
                    fac_det_c_iid = table.Column<int>(type: "int", nullable: false),
                    fac_c_iid = table.Column<int>(type: "int", nullable: false),
                    fac_det_c_iitem = table.Column<int>(type: "int", nullable: false),
                    fac_det_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_det_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_FACTURA_DETALLE", x => x.fac_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_FACTURA_DETALLE_SIC_T_FACTURA",
                        column: x => x.fac_c_iid,
                        principalSchema: "Venta",
                        principalTable: "SIC_T_FACTURA",
                        principalColumn: "fac_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_BOLETA_DETALLE",
                schema: "Facturacion",
                columns: table => new
                {
                    bol_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bol_c_iid = table.Column<int>(type: "int", nullable: false),
                    bol_det_c_iitem = table.Column<int>(type: "int", nullable: false),
                    bol_det_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_det_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    bol_det_c_epreciotot = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_BOLETA_DETALLE", x => x.bol_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_BOLETA_DETALLE_SIC_T_BOLETA",
                        column: x => x.bol_c_iid,
                        principalSchema: "Facturacion",
                        principalTable: "SIC_T_BOLETA",
                        principalColumn: "bol_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_BOLETA_DETALLE_SIC_T_ITEM1",
                        column: x => x.bol_det_c_iitem,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ESTILO",
                schema: "Confeccion",
                columns: table => new
                {
                    esti_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esti_c_vcodigo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    esti_c_vnombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    esti_c_vdescripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    itm_c_iid = table.Column<int>(type: "int", nullable: false),
                    color_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    cate_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    talla_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ESTILO", x => x.esti_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO_CATEGORIA",
                        column: x => x.cate_c_vid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_CATEGORIA",
                        principalColumn: "cate_c_vid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO_COLOR",
                        column: x => x.color_c_vid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_COLOR",
                        principalColumn: "color_c_vid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO_ITEM",
                        column: x => x.itm_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO_TALLA",
                        column: x => x.talla_c_vid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_TALLA",
                        principalColumn: "talla_c_vid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_FACTURA_DETALLE",
                schema: "Facturacion",
                columns: table => new
                {
                    fac_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fac_c_iid = table.Column<int>(type: "int", nullable: false),
                    fac_det_c_iitem = table.Column<int>(type: "int", nullable: false),
                    fac_det_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_det_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    fac_det_c_epreciotot = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_FACTURA_DETALLE", x => x.fac_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_FACTURA_DETALLE_SIC_T_FACTURA",
                        column: x => x.fac_c_iid,
                        principalSchema: "Facturacion",
                        principalTable: "SIC_T_FACTURA",
                        principalColumn: "fac_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_FACTURA_DETALLE_SIC_T_ITEM",
                        column: x => x.fac_det_c_iitem,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ITEM_ALMACEN",
                schema: "Mantenimiento",
                columns: table => new
                {
                    itm_alm_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itm_c_iid = table.Column<int>(type: "int", nullable: false),
                    alm_c_iid = table.Column<int>(type: "int", nullable: false),
                    itm_alm_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ITEM_ALMACEN", x => x.itm_alm_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ITEM_ALMACEN_SIC_T_ALMACEN",
                        column: x => x.alm_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ALMACEN",
                        principalColumn: "alm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ITEM_ALMACEN_SIC_T_ITEM",
                        column: x => x.itm_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MOVIMIENTO_SALIDA_DETALLE",
                schema: "Almacen",
                columns: table => new
                {
                    mvs_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mvs_c_iid = table.Column<int>(type: "int", nullable: false),
                    alm_c_iid = table.Column<int>(type: "int", nullable: false),
                    itm_c_iid = table.Column<int>(type: "int", nullable: false),
                    mvs_det_c_ecant = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MOVIMIENTO_SALIDA_DETALLE", x => x.mvs_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_ALMACEN",
                        column: x => x.alm_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ALMACEN",
                        principalColumn: "alm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_ITEM",
                        column: x => x.itm_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_SALIDA_DETALLE_SIC_T_MOVIMIENTO_SALIDA",
                        column: x => x.mvs_c_iid,
                        principalSchema: "Almacen",
                        principalTable: "SIC_T_MOVIMIENTO_SALIDA",
                        principalColumn: "mvs_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ORDEN_DE_COMPRA_DET",
                schema: "Mantenimiento",
                columns: table => new
                {
                    odc_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    odc_c_iid = table.Column<int>(type: "int", nullable: false),
                    odc_c_iitemid = table.Column<int>(type: "int", nullable: false),
                    odc_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    odc_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    odc_c_epreciototal = table.Column<decimal>(type: "decimal(19,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ORDEN_DE_COMPRA_DET_1", x => x.odc_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_DET_SIC_T_ITEM",
                        column: x => x.odc_c_iitemid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_ORDEN_DE_COMPRA_DET_SIC_T_ORDEN_DE_COMPRA",
                        column: x => x.odc_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ORDEN_DE_COMPRA",
                        principalColumn: "odc_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_VENTA_DETALLE",
                schema: "Mantenimiento",
                columns: table => new
                {
                    ven_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ven_c_iid = table.Column<int>(type: "int", nullable: false),
                    ven_det_c_iitemid = table.Column<int>(type: "int", nullable: false),
                    ven_det_c_ecantidad = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    ven_det_c_epreciounit = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    ven_det_c_epreciototal = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    ven_det_c_iidalmacen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_VENTA_DETALLE", x => x.ven_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_DETALLE_SIC_T_ALMACEN",
                        column: x => x.ven_det_c_iidalmacen,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ALMACEN",
                        principalColumn: "alm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_DETALLE_SIC_T_ITEM",
                        column: x => x.ven_det_c_iitemid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ITEM",
                        principalColumn: "itm_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_VENTA_DETALLE_SIC_T_VENTA",
                        column: x => x.ven_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_VENTA",
                        principalColumn: "ven_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ESTILO_PROCESO",
                schema: "Confeccion",
                columns: table => new
                {
                    esti_proceso_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esti_c_iid = table.Column<int>(type: "int", nullable: false),
                    proc_c_vid = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    esti_proceso_c_ecosto = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ESTILO_PROCESO", x => x.esti_proceso_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO",
                        column: x => x.esti_c_iid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_ESTILO",
                        principalColumn: "esti_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_PROCESO",
                        column: x => x.proc_c_vid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_PROCESO",
                        principalColumn: "proc_c_vid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_MOVIMIENTO_ENTRADA_DETALLE",
                schema: "Mantenimiento",
                columns: table => new
                {
                    mve_det_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mve_c_iid = table.Column<int>(type: "int", nullable: false),
                    mve_c_ecant_pedida = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    mve_c_ecant_recibida = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    mve_c_vdescripcion_item = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mve_c_iocdet_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_MOVIMIENTO_ENTRADA_DETALLE", x => x.mve_det_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_SIC_T_MOVIMIENTO_ENTRADA",
                        column: x => x.mve_c_iid,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_MOVIMIENTO_ENTRADA",
                        principalColumn: "mve_c_iid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_SIC_T_ORDEN_DE_COMPRA_DET",
                        column: x => x.mve_c_iocdet_id,
                        principalSchema: "Mantenimiento",
                        principalTable: "SIC_T_ORDEN_DE_COMPRA_DET",
                        principalColumn: "odc_det_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SIC_T_ESTILO_PROCESO_DETALLE",
                schema: "Confeccion",
                columns: table => new
                {
                    esti_proc_detalle_c_iid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esti_proceso_c_iid = table.Column<int>(type: "int", nullable: false),
                    esti_proc_detalle_c_vdescripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    esti_proc_detalle_c_vmaquina = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    esti_proc_detalle_c_ecosto = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    esti_proc_detalle_c_isegundos = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    esti_proceso_c_yorden = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIC_T_ESTILO_PROCESO_DETALLE", x => x.esti_proc_detalle_c_iid);
                    table.ForeignKey(
                        name: "FK_SIC_T_ESTILO_PROCESO_DETALLE",
                        column: x => x.esti_proceso_c_iid,
                        principalSchema: "Confeccion",
                        principalTable: "SIC_T_ESTILO_PROCESO",
                        principalColumn: "esti_proceso_c_iid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ALMACEN_CENTRO_COSTO_alm_cst_c_iid_almacen",
                table: "SIC_T_ALMACEN_CENTRO_COSTO",
                column: "alm_cst_c_iid_almacen");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ALMACEN_CENTRO_COSTO_alm_cst_c_iid_centro_costo",
                table: "SIC_T_ALMACEN_CENTRO_COSTO",
                column: "alm_cst_c_iid_centro_costo");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_BOLETA_bol_c_iventa",
                schema: "Facturacion",
                table: "SIC_T_BOLETA",
                column: "bol_c_iventa");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_BOLETA_bol_c_iventa1",
                schema: "Venta",
                table: "SIC_T_BOLETA",
                column: "bol_c_iventa");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_BOLETA_DETALLE_bol_c_iid",
                schema: "Facturacion",
                table: "SIC_T_BOLETA_DETALLE",
                column: "bol_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_BOLETA_DETALLE_bol_det_c_iitem",
                schema: "Facturacion",
                table: "SIC_T_BOLETA_DETALLE",
                column: "bol_det_c_iitem");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_BOLETA_DETALLE_bol_c_iid1",
                schema: "Venta",
                table: "SIC_T_BOLETA_DETALLE",
                column: "bol_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLI_CONTACTO_cli_c_vdoc_id",
                schema: "Mantenimiento",
                table: "SIC_T_CLI_CONTACTO",
                column: "cli_c_vdoc_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLI_CONTACTO_cli_contac_cargo_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_CLI_CONTACTO",
                column: "cli_contac_cargo_c_yid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLI_DIRECCION_dist_c_ccod_ubig",
                schema: "Mantenimiento",
                table: "SIC_T_CLI_DIRECCION",
                column: "dist_c_ccod_ubig");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLIENTE_cli_scor_c_cletra",
                schema: "Mantenimiento",
                table: "SIC_T_CLIENTE",
                column: "cli_scor_c_cletra");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLIENTE_colab_c_cdoc_id",
                schema: "Mantenimiento",
                table: "SIC_T_CLIENTE",
                column: "colab_c_cdoc_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_CLIENTE_zona_rep_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_CLIENTE",
                column: "zona_rep_c_yid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_COLABORADOR_colab_area_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_COLABORADOR",
                column: "colab_area_c_yid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_COLABORADOR_colab_cargo_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_COLABORADOR",
                column: "colab_cargo_c_yid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_DISTRITO_prov_c_ccod",
                schema: "Mantenimiento",
                table: "SIC_T_DISTRITO",
                column: "prov_c_ccod");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_EMP_DIRECCION_emp_dir_c_iid_centrocosto",
                schema: "Mantenimiento",
                table: "SIC_T_EMP_DIRECCION",
                column: "emp_dir_c_iid_centrocosto");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_cate_c_vid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "cate_c_vid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_color_c_vid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "color_c_vid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_itm_c_iid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "itm_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_talla_c_vid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "talla_c_vid");

            migrationBuilder.CreateIndex(
                name: "UQ_SIC_T_ESTILO_CODIGO",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "esti_c_vcodigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_SIC_T_ESTILO_NOMBRE",
                schema: "Confeccion",
                table: "SIC_T_ESTILO",
                column: "esti_c_vnombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_PROCESO_esti_c_iid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO_PROCESO",
                column: "esti_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_PROCESO_proc_c_vid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO_PROCESO",
                column: "proc_c_vid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ESTILO_PROCESO_DETALLE_esti_proceso_c_iid",
                schema: "Confeccion",
                table: "SIC_T_ESTILO_PROCESO_DETALLE",
                column: "esti_proceso_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_FACTURA_fac_c_iventa",
                schema: "Facturacion",
                table: "SIC_T_FACTURA",
                column: "fac_c_iventa");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_FACTURA_fac_c_iventa1",
                schema: "Venta",
                table: "SIC_T_FACTURA",
                column: "fac_c_iventa");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_FACTURA_DETALLE_fac_c_iid",
                schema: "Facturacion",
                table: "SIC_T_FACTURA_DETALLE",
                column: "fac_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_FACTURA_DETALLE_fac_det_c_iitem",
                schema: "Facturacion",
                table: "SIC_T_FACTURA_DETALLE",
                column: "fac_det_c_iitem");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_FACTURA_DETALLE_fac_c_iid1",
                schema: "Venta",
                table: "SIC_T_FACTURA_DETALLE",
                column: "fac_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_pro_partida_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM",
                column: "pro_partida_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_und_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM",
                column: "und_c_yid");

            migrationBuilder.CreateIndex(
                name: "UQ__SIC_T_IT__28CD5C006EF57B66",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM",
                column: "itm_c_ccodigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_ALMACEN_alm_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM_ALMACEN",
                column: "alm_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_ALMACEN_itm_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM_ALMACEN",
                column: "itm_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_FAMILIA_segmento_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM_FAMILIA",
                column: "segmento_c_yid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ITEM_SUB_FAMILIA_isf_c_ifm_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ITEM_SUB_FAMILIA",
                column: "isf_c_ifm_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MENU_menu_c_iid_padre",
                schema: "Seguridad",
                table: "SIC_T_MENU",
                column: "menu_c_iid_padre");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_ENTRADA_mve_c_iestado",
                schema: "Mantenimiento",
                table: "SIC_T_MOVIMIENTO_ENTRADA",
                column: "mve_c_iestado");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_ENTRADA_mve_c_iidalmacen",
                schema: "Mantenimiento",
                table: "SIC_T_MOVIMIENTO_ENTRADA",
                column: "mve_c_iidalmacen");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_ENTRADA_odc_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_MOVIMIENTO_ENTRADA",
                column: "odc_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_mve_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_MOVIMIENTO_ENTRADA_DETALLE",
                column: "mve_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_ENTRADA_DETALLE_mve_c_iocdet_id",
                schema: "Mantenimiento",
                table: "SIC_T_MOVIMIENTO_ENTRADA_DETALLE",
                column: "mve_c_iocdet_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_cli_c_vdoc_id",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA",
                column: "cli_c_vdoc_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_mov_estado_iid",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA",
                column: "mov_estado_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_ven_c_iid",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA",
                column: "ven_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_DETALLE_alm_c_iid",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA_DETALLE",
                column: "alm_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_DETALLE_itm_c_iid",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA_DETALLE",
                column: "itm_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_MOVIMIENTO_SALIDA_DETALLE_mvs_c_iid",
                schema: "Almacen",
                table: "SIC_T_MOVIMIENTO_SALIDA_DETALLE",
                column: "mvs_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_emp_dir_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA",
                column: "emp_dir_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_odc_c_clase_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA",
                column: "odc_c_clase_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_odc_c_iestado",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA",
                column: "odc_c_iestado");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_prov_c_vdoc_id",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA",
                column: "prov_c_vdoc_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_DET_odc_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA_DET",
                column: "odc_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ORDEN_DE_COMPRA_DET_odc_c_iitemid",
                schema: "Mantenimiento",
                table: "SIC_T_ORDEN_DE_COMPRA_DET",
                column: "odc_c_iitemid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_PERFIL_MENU_menu_c_iid",
                schema: "Seguridad",
                table: "SIC_T_PERFIL_MENU",
                column: "menu_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_PERFIL_OPCION_opc_c_iid",
                schema: "Seguridad",
                table: "SIC_T_PERFIL_OPCION",
                column: "opc_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_PRODUCTO_PARTIDA_isf_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_PRODUCTO_PARTIDA",
                column: "isf_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_PROVINCIA_depa_c_ccod",
                schema: "Mantenimiento",
                table: "SIC_T_PROVINCIA",
                column: "depa_c_ccod");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_USUARIO_OPCION_opc_c_iid",
                schema: "Seguridad",
                table: "SIC_T_USUARIO_OPCION",
                column: "opc_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_ven_c_icentrocosto",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA",
                column: "ven_c_icentrocosto");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_ven_c_iestado",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA",
                column: "ven_c_iestado");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_ven_c_vdoccli_id",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA",
                column: "ven_c_vdoccli_id");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_DETALLE_ven_c_iid",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA_DETALLE",
                column: "ven_c_iid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_DETALLE_ven_det_c_iidalmacen",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA_DETALLE",
                column: "ven_det_c_iidalmacen");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_VENTA_DETALLE_ven_det_c_iitemid",
                schema: "Mantenimiento",
                table: "SIC_T_VENTA_DETALLE",
                column: "ven_det_c_iitemid");

            migrationBuilder.CreateIndex(
                name: "IX_SIC_T_ZONA_REPARTO_LUGAR_zona_rep_c_yid",
                schema: "Mantenimiento",
                table: "SIC_T_ZONA_REPARTO_LUGAR",
                column: "zona_rep_c_yid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SIC_T_ALMACEN_CENTRO_COSTO");

            migrationBuilder.DropTable(
                name: "SIC_T_BOLETA_DETALLE",
                schema: "Facturacion");

            migrationBuilder.DropTable(
                name: "SIC_T_BOLETA_DETALLE",
                schema: "Venta");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_CONTACTO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_DIRECCION",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_NOMB_COM",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_RS_HISTORICO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CONCEPTO",
                schema: "General");

            migrationBuilder.DropTable(
                name: "SIC_T_EMPRESA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ESTILO_PROCESO_DETALLE",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_FACTURA_DETALLE",
                schema: "Facturacion");

            migrationBuilder.DropTable(
                name: "SIC_T_FACTURA_DETALLE",
                schema: "Venta");

            migrationBuilder.DropTable(
                name: "SIC_T_IGV",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_IMPRESORA",
                schema: "General");

            migrationBuilder.DropTable(
                name: "SIC_T_ITEM_ALMACEN",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_MOVIMIENTO_ENTRADA_DETALLE",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_MOVIMIENTO_SALIDA_DETALLE",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "SIC_T_NOMB_COM",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_PARAMETRO_DET",
                schema: "General");

            migrationBuilder.DropTable(
                name: "SIC_T_PERFIL_MENU",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_PERFIL_OPCION",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_TASA_CAMBIO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_USUARIO_OPCION",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_USUARIO_PERFIL",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_VENTA_DETALLE",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ZONA_REPARTO_LUGAR",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_BOLETA",
                schema: "Facturacion");

            migrationBuilder.DropTable(
                name: "SIC_T_BOLETA",
                schema: "Venta");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_CONTAC_CARGO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_DISTRITO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ESTILO_PROCESO",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_FACTURA",
                schema: "Facturacion");

            migrationBuilder.DropTable(
                name: "SIC_T_FACTURA",
                schema: "Venta");

            migrationBuilder.DropTable(
                name: "SIC_T_MOVIMIENTO_ENTRADA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ORDEN_DE_COMPRA_DET",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_MOVIMIENTO_SALIDA",
                schema: "Almacen");

            migrationBuilder.DropTable(
                name: "SIC_T_PARAMETRO",
                schema: "General");

            migrationBuilder.DropTable(
                name: "SIC_T_MENU",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_PERFIL",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_OPCION",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_USUARIO",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "SIC_T_PROVINCIA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ESTILO",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_PROCESO",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_ALMACEN",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ORDEN_DE_COMPRA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_MOV_ESTADO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_VENTA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_DEPARTAMENTO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CATEGORIA",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_COLOR",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_ITEM",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_TALLA",
                schema: "Confeccion");

            migrationBuilder.DropTable(
                name: "SIC_T_EMP_DIRECCION",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ODC_CLASE",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ODC_ESTADO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CLIENTE",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_VEN_ESTADO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_PRODUCTO_PARTIDA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_UNIDAD_MEDIDA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_EMP_CENTRO_COSTO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_CLI_SCORING",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_COLABORADOR",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ZONA_REPARTO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ITEM_SUB_FAMILIA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_COLAB_AREA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_COLAB_CARGO",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_ITEM_FAMILIA",
                schema: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "SIC_T_SEGMENTO",
                schema: "Mantenimiento");
        }
    }
}
