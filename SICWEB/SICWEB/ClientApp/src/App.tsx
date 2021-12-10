import "./App.css";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";
import { AuthProvider } from "src/contexts/JWTAuthContext";
import { SnackbarProvider } from "notistack";
import routes, { renderRoutes } from "./routes";
import MomentUtils from "@date-io/moment";
// import DateFnsUtils from '@date-io/date-fns';
import { MuiPickersUtilsProvider } from "@material-ui/pickers";
import "moment/locale/es-mx";
import moment from "moment";
const history = createBrowserHistory();
moment.locale("es-mx");
function App() {
  return (
    <MuiPickersUtilsProvider utils={MomentUtils} locale={"es-mx"}>
      <SnackbarProvider dense maxSnack={3}>
        <Router history={history}>
          <AuthProvider>{renderRoutes(routes)}</AuthProvider>
        </Router>
      </SnackbarProvider>
    </MuiPickersUtilsProvider>
  );
}

export default App;
