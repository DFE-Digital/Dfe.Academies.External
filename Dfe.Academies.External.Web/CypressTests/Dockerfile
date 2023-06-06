FROM cypress/base:16.17.0

COPY ./cypress ./cypress
COPY customConfig.ts customConfig.ts
COPY cypress.config.ts cypress.config.ts
COPY package-lock.json package-lock.json
COPY package.json package.json
COPY tsconfig.json tsconfig.json

RUN npm install
ENTRYPOINT ["npm", "run", "cy:run"]