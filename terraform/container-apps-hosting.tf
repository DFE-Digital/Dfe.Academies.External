module "azure_container_apps_hosting" {
  source = "github.com/DFE-Digital/terraform-azurerm-container-apps-hosting?ref=v0.17.1"

  environment    = local.environment
  project_name   = local.project_name
  azure_location = local.azure_location
  tags           = local.tags

  virtual_network_address_space = local.virtual_network_address_space

  enable_container_registry = local.enable_container_registry

  image_name                             = local.image_name
  container_command                      = local.container_command
  container_secret_environment_variables = local.container_secret_environment_variables

  enable_redis_cache = local.enable_redis_cache

  enable_event_hub = local.enable_event_hub

  enable_dns_zone      = local.enable_dns_zone
  dns_zone_domain_name = local.dns_zone_domain_name
  dns_ns_records       = local.dns_ns_records
  dns_txt_records      = local.dns_txt_records
  dns_a_records        = local.dns_a_records

  enable_cdn_frontdoor                        = local.enable_cdn_frontdoor
  cdn_frontdoor_enable_rate_limiting          = local.cdn_frontdoor_enable_rate_limiting
  cdn_frontdoor_rate_limiting_threshold       = local.cdn_frontdoor_rate_limiting_threshold
  cdn_frontdoor_host_add_response_headers     = local.cdn_frontdoor_host_add_response_headers
  cdn_frontdoor_custom_domains                = local.cdn_frontdoor_custom_domains
  cdn_frontdoor_host_redirects                = local.cdn_frontdoor_host_redirects
  restrict_container_apps_to_cdn_inbound_only = local.restrict_container_apps_to_cdn_inbound_only

  enable_monitoring              = local.enable_monitoring
  monitor_email_receivers        = local.monitor_email_receivers
  monitor_enable_slack_webhook   = local.monitor_enable_slack_webhook
  monitor_slack_webhook_receiver = local.monitor_slack_webhook_receiver
  monitor_slack_channel          = local.monitor_slack_channel

  enable_container_app_blob_storage = local.enable_container_app_blob_storage

  existing_network_watcher_name                = local.existing_network_watcher_name
  existing_network_watcher_resource_group_name = local.existing_network_watcher_resource_group_name
}
