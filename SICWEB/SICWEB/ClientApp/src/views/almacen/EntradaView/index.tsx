import type { FC } from 'react';
import {
  Container,
  makeStyles
} from '@material-ui/core';
import Page from 'src/components/Page';
import type { Theme } from 'src/theme';
import Header from './Header';
import Tables from "./Tables";


const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default
  }
}));

const EntradaView: FC = () => {

  const classes = useStyles();

  return (
    <Page
      className={classes.root}
      title="Settings"
    >
      <Container maxWidth={false}>
        <Header />
        <Tables />
      </Container>
    </Page>
  );
};
export default EntradaView;