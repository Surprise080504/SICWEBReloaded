import type { FC } from 'react';
import { Link as RouterLink } from 'react-router-dom';
import {
  Link
} from '@material-ui/core';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import Typography from '@material-ui/core/Typography';

const Header: FC = () => {

  return (
    <Breadcrumbs
      separator={<NavigateNextIcon fontSize="small" />}
      aria-label="breadcrumb"
    >
      <Link
        variant="body1"
        color="inherit"
        to="/app"
        component={RouterLink}
      >
        SEGURIDAD
      </Link>
      <Typography
        variant="body1"
        color="textPrimary"
      >
        MENÚS
      </Typography>
    </Breadcrumbs>
  );
};
export default Header;