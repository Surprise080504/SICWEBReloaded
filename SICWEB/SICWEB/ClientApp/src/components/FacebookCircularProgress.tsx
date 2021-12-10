import React from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import CircularProgress, { CircularProgressProps } from '@material-ui/core/CircularProgress';

const useStylesFacebook = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      justifyContent: 'center'
    },
    top: {
      color: '#1a90ff',
      animationDuration: '550ms',
      left: 0,
    },
    circle: {
      strokeLinecap: 'round',
    },
  }),
);

function FacebookCircularProgress(props: CircularProgressProps) {
  const classes = useStylesFacebook();

  return (
    <div className={classes.root}>
      <CircularProgress
        variant="indeterminate"
        disableShrink
        className={classes.top}
        classes={{
          circle: classes.circle,
        }}
        size={40}
        thickness={4}
        {...props}
      />
    </div>
  );
}

export default FacebookCircularProgress;
