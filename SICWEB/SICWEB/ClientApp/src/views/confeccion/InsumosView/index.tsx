import type { FC } from "react";
import { Container, makeStyles } from "@material-ui/core";
import Page from "src/components/Page";
import type { Theme } from "src/theme";
import { useParams } from "react-router-dom";
import Header from "./Header";
import InsumosTable from "./InsumosTable";
// import EstiloTable from './EstiloTable';

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
}));

interface InsumosViewProps {
  location?: any;
}

const InsumosView: FC<InsumosViewProps> = (props) => {
  const { estiloId } = useParams<{ estiloId: string }>();
  const estilo = props.location.state;

  const classes = useStyles();

  const products = [];
  return (
    <Page className={classes.root} title="Settings">
      <Container maxWidth={false}>
        <Header />
        <InsumosTable _estilo={estilo} />
      </Container>
    </Page>
  );
};
export default InsumosView;
