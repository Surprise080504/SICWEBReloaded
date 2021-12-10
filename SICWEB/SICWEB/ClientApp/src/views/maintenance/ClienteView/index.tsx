import type { FC } from "react";
import { Container, makeStyles } from "@material-ui/core";
import Page from "src/components/Page";
import type { Theme } from "src/theme";
import Header from "./Header";
import ClienteTable from "./ClienteTable";

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default,
  },
}));

const ClienteView: FC = () => {
  const classes = useStyles();
  const clientes = [];
  return (
    <Page className={classes.root} title="Settings">
      <Container maxWidth={false}>
        <Header />
        <ClienteTable />
      </Container>
    </Page>
  );
};
export default ClienteView;
