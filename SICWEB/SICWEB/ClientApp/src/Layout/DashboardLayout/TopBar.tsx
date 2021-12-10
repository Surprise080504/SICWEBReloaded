import type { FC } from 'react';
import clsx from 'clsx';
import PropTypes from 'prop-types';
import {
  AppBar,
  Button,
  Toolbar,
  makeStyles
} from '@material-ui/core';
import MenuItems from './MenuItems';

interface TopBarProps {
  className?: string;
}

const useStyles = makeStyles((theme) => ({
  root: {
    backgroundColor: theme.palette.background.default
  },
  toolbar: {
    height: 64
  },
  toolbar2: {
    marginTop: 1,
    height: 45,
    minHeight: 40,
    borderTop: '1px solid'
  },
  [theme.breakpoints.down(583)]: {
    toolbar2: {
      height: 100
    }
  },
  logo: {
    marginRight: theme.spacing(2)
  },
  link: {
    fontWeight: theme.typography.fontWeightMedium,
    '& + &': {
      marginLeft: theme.spacing(2)
    }
  },
  divider: {
    width: 1,
    height: 32,
    marginLeft: theme.spacing(2),
    marginRight: theme.spacing(2)
  }
}));

const TopBar: FC<TopBarProps> = ({ className, ...rest }) => {
  const classes = useStyles();

  return (
    <AppBar
      className={clsx(classes.root, className)}
      color="default"
      {...rest}
    >
      <Toolbar className={classes.toolbar}>
        <Button
          color="secondary"
          component="a"
          href="/"
          variant="contained"
          size="small"
        >
          
        </Button>
      </Toolbar>
      <Toolbar className={classes.toolbar2}>
        <MenuItems/>
      </Toolbar>
    </AppBar>
  );
};

TopBar.propTypes = {
  className: PropTypes.string
};

export default TopBar;
