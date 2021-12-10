import type { FC } from "react";
import { Container, makeStyles } from "@material-ui/core";
import Page from "src/components/Page";
import type { Theme } from "src/theme";
import { useParams, useHistory } from "react-router-dom";
import Header from "./Header";
import ProcessTable from "./ProcessTable";
// import EstiloTable from './EstiloTable';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
}));

interface ProcessViewProps {
  location?: any;
}

const ProcessView: FC<ProcessViewProps> = (props) => {
  const { estiloId } = useParams<{ estiloId: string }>();
  const estilo = props.location.state;

  const classes = useStyles();

  const products = [];

  return (
    <Page className={classes.root} title="Settings">
      <Container maxWidth={false}>
        <Header />
        <ProcessTable _estilo={estilo} />
      </Container>
    </Page>
  );
};
export default ProcessView;
