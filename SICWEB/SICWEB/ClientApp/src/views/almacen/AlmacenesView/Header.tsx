import type { FC } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import {
  Link,Grid
} from '@material-ui/core';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import Typography from '@material-ui/core/Typography';

const Header: FC = () => {

  return (
    <Grid>
      <Breadcrumbs
        separator={<NavigateNextIcon fontSize="small" />}
        aria-label="breadcrumb"
      >
        <Link
          variant="body1"
          color="inherit"
          to="/"
          component={RouterLink}
        >
          MANTENIMIENTO
        </Link>
        <Typography
          variant="body1"
          color="textPrimary"
        >
          Almacenes
        </Typography>
      </Breadcrumbs>      
    </Grid>    
  );
};
export default Header;