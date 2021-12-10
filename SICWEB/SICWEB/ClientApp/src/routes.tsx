import React, { Suspense, Fragment, lazy } from "react";
import { Switch, Route } from "react-router-dom";

import AuthGuard from "src/components/AuthGuard";
import GuestGuard from "src/components/GuestGuard";
import MainGuard from "src/components/MainGuard";

import DashboardLayout from "./Layout/DashboardLayout";

import LoadingScreen from "./components/LoadingScreen";

type Routes = {
  exact?: boolean;
  path?: string | string[];
  guard?: any;
  layout?: any;
  component?: any;
  routes?: Routes;
}[];

export const renderRoutes = (routes: Routes = []): JSX.Element => {
  return (
    <Suspense fallback={<LoadingScreen />}>
      <Switch>
        {routes.map((route, i) => {
          const Guard = route.guard || Fragment;
          const Layout = route.layout || Fragment;
          const Component = route.component;

          return (
            <Route
              key={i}
              path={route.path}
              exact={route.exact}
              render={(props) => (
                <Guard>
                  <Layout>
                    {route.routes ? (
                      renderRoutes(route.routes)
                    ) : (
                      <Component {...props} />
                    )}
                  </Layout>
                </Guard>
              )}
            />
          );
        })}
      </Switch>
    </Suspense>
  );
};

const routes: Routes = [
  {
    exact: true,
    path: "/404",
    component: lazy(() => import("src/views/errors/NotFoundView")),
  },
  {
    exact: true,
    guard: GuestGuard,
    path: "/login",
    component: lazy(() => import("src/views/auth/LoginView")),
  },
  {
    exact: true,
    guard: GuestGuard,
    path: "/register",
    component: lazy(() => import("src/views/auth/RegisterView")),
  },
  {
    path: "/principal",
    guard: AuthGuard,
    layout: DashboardLayout,
    routes: [
      {
        exact: true,
        path: "/principal/tablero",
        component: lazy(() => import("src/views/app/DashboardView")),
      },
    ],
  },
  {
    path: "/interfaces",
    guard: AuthGuard,
    layout: DashboardLayout,
    routes: [
      //SEGURIDAD
      {
        exact: true, // MENÚS
        path: "/interfaces/Seguridad/frmMenus",
        component: lazy(() => import("src/views/Seguridad/MenusView")),
      },
      {
        exact: true, // USUARIOS
        path: "/interfaces/Seguridad/frmUsuarios",
        component: lazy(() => import("src/views/Seguridad/UsuariosView")),
      },
      //MANTENIMIENTO
      {
        exact: true, //CLIENTE / PROVEEDOR
        path: "/interfaces/mantenimiento/frmRegCliente",
        component: lazy(() => import("src/views/maintenance/ClienteView")),
      },
      {
        exact: true, //ITEM
        path: "/interfaces/mantenimiento/frmregitem",
        component: lazy(() => import("src/views/maintenance/ItemView")),
      },
      {
        exact: true, //EMPRESA
        path: "/interfaces/mantenimiento/frmMantEmpresa",
        component: lazy(() => import("src/views/maintenance/EmpresaView")),
      },
      {
        exact: true, //MANTENIMIENTO IMPRESORAS
        path: "/interfaces/mantenimiento/frmMantImpresoras",
        component: lazy(() => import("src/views/maintenance/ImpresorasView")),
      },
      {
        exact: true, //ESTILOS
        path: "/interfaces/mantenimiento/frmRegEstilo",
        component: lazy(() => import("src/views/maintenance/EstiloView")),
      },
      //ALMAC�N
      {
        exact: true, //MOVIMIENTO DE ENTRADA
        path: "/interfaces/almacen/frmMovEntrada",
        component: lazy(() => import("src/views/almacen/EntradaView")),
      },
      {
        exact: true, //ALMACENES
        path: "/interfaces/almacen/frmAlmacenes",
        component: lazy(() => import("src/views/almacen/AlmacenesView")),
      },
      {
        exact: true, //MOVIMIENTO DE SALIDA
        path: "/interfaces/almacen/frmMovSalida",
        component: lazy(() => import("src/views/almacen/SalidaView")),
      },
      //FACTURACI�N
      {
        exact: true, //VENTA
        path: "/interfaces/facturacion/frmVenta",
        component: lazy(() => import("src/views/facturacion/VentaView")),
      },
      {
        exact: true, //FACTURACI�N AUTOMATICA
        path: "/interfaces/facturacion/frmFactAutomatica",
        component: lazy(() => import("src/views/facturacion/AutomaticaView")),
      },
      {
        exact: true, //FACTURACI�N IMPRESI�N
        path: "/interfaces/facturacion/frmFactImpresion",
        component: lazy(() => import("src/views/facturacion/ImpresionView")),
      },
      //COMPRAS
      {
        exact: true, //ORDEN DE COMPRA
        path: "/interfaces/compras/frmRegOC",
        component: lazy(() => import("src/views/compras/OCView")),
      },
      //CONFECCION
      {
        exact: true, //ESTILOS
        path: "/interfaces/confeccion/frmRegEstilo",
        component: lazy(() => import("src/views/confeccion/EstiloView")),
      },
      {
        exact: true, //PROCESS
        path: "/interfaces/confeccion/process/:estiloId",
        component: lazy(() => import("src/views/confeccion/ProcessView")),
      },
      {
        exact: true, //INSUMOS
        path: "/interfaces/confeccion/insumos/:estiloId",
        component: lazy(() => import("src/views/confeccion/InsumosView")),
      },
    ],
  },
  {
    exact: true,
    path: "/",
    guard: MainGuard,
    component: lazy(() => import("src/views/auth/LoginView")),
  },
];

export default routes;
