import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import store from 'src/store';
import { SettingsProvider } from 'src/contexts/SettingsContext';
import 'react-perfect-scrollbar/dist/css/styles.css';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

ReactDOM.render(
  <Provider store={store}>
    <SettingsProvider>
      <App />
    </SettingsProvider>
  </Provider>,
  document.getElementById('root')
);

reportWebVitals();
