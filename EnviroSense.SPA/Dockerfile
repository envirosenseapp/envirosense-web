# Multi-stage build for Angular SPA -> Nginx web server image

# 1) Build stage
FROM node:22-alpine AS builder

WORKDIR /app

# Install deps first (better layer caching)
COPY package*.json ./
RUN npm ci --no-audit --no-fund

# Copy sources and build
COPY . .
RUN npm run build

# 2) Runtime stage
FROM nginx:1.27-alpine AS runtime

# Copy custom nginx config for SPA routing
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy build output from builder stage
# Angular outputs client files under dist/<app>/browser
COPY --from=builder /app/dist/envirosense-spa/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]


