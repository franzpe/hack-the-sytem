import React, { FC } from 'react';
import { AppProps } from 'next/app';
import '../styles/main.scss';

const App: FC<AppProps> = ({ Component, pageProps }) => {
  return <Component {...pageProps} />;
};

export default App;
