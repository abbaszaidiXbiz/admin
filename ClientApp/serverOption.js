import { env } from 'process';

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:8850';

export default {
https: { pfx: 'devcert.pfx', passphrase: 'caafb9c3dc924d69b5e303a301ba4d1f' },
// proxy: {
//     '/api': {
//       target: target,
//       changeOrigin: true,
//       secure: false,
//       ws: true,
//     }
//   }


}

