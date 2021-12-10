import { combineReducers } from '@reduxjs/toolkit';
import { reducer as businessReducer } from 'src/slices/business';

const rootReducer = combineReducers({
  businesses: businessReducer
});

export default rootReducer;
