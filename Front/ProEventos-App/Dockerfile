# Estágio 1: Build da aplicação Angular
FROM node:14-alpine AS build
WORKDIR /app

# Copie package.json e package-lock.json
# Se você usa yarn, copie yarn.lock em vez de package-lock.json e use 'RUN yarn install'
COPY package.json ./
COPY package-lock.json ./

# Instale as dependências
RUN npm install

# Copie todo o código fonte do frontend
COPY . .


# Ajuste o caminho no comando COPY do estágio 2 se necessário.
RUN npm run build -- --configuration production

# Estágio 2: Servir a aplicação com Nginx
FROM nginx:1.25-alpine
WORKDIR /usr/share/nginx/html

# Remova o conteúdo padrão do Nginx
RUN rm -rf ./*

# Copie os arquivos construídos do estágio 'build'
# Ajuste 'dist/pro-eventos-app' abaixo se o seu outputPath for diferente!
COPY --from=build /app/dist/ProEventos-App/ .
# Se o nome do projeto no angular.json (e consequentemente na pasta dist) for "ProEventos-App", use:
# COPY --from=build /app/dist/ProEventos-App/ .

# Copie a configuração customizada do Nginx para lidar com o roteamento do Angular
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Exponha a porta 80 (porta padrão do Nginx)
EXPOSE 80

# Comando padrão para iniciar o Nginx
CMD ["nginx", "-g", "daemon off;"]