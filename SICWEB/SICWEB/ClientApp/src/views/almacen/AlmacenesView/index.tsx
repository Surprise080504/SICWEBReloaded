import type { FC } from 'react';
import {
  Container,
  makeStyles
} from '@material-ui/core';
import Page from 'src/components/Page';
import type { Theme } from 'src/theme';
import Header from './Header';


const useStyles = makeStyles((theme: Theme) => ({
  root: {
    backgroundColor: theme.palette.background.default
  }
}));

const AlmacenesView: FC = () => {
  
  const classes = useStyles();

  const products = [];
  return (
    <Page
      className={classes.root}
      title="Settings"
    >
      <Container maxWidth={false}>
        <Header />
        En desarrollo..
      </Container>
    </Page>
  );
};
export default AlmacenesView;