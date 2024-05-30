module "azure_container_apps_hosting" {
  source = "github.com/DFE-Digital/terraform-azurerm-container-apps-hosting?ref=v1.6.4"

  environment    = local.environment
  project_name   = local.project_name
  azure_location = local.azure_location
  tags           = local.tags

  virtual_network_address_space = local.virtual_network_address_space

  enable_container_registry             = local.enable_container_registry
  image_name                            = local.image_name
  registry_server                       = local.registry_server
  registry_admin_enabled                = local.registry_admin_enabled
  registry_use_managed_identity         = local.registry_use_managed_identity
  registry_managed_identity_assign_role = local.registry_managed_identity_assign_role

  container_command                      = local.container_command
  container_secret_environment_variables = local.container_secret_environment_variables
  container_max_replicas                 = local.container_max_replicas
  container_scale_http_concurrency       = local.container_scale_http_concurrency

  enable_redis_cache = local.enable_redis_cache

  enable_event_hub                          = local.enable_event_hub
  enable_logstash_consumer                  = local.enable_logstash_consumer
  eventhub_export_log_analytics_table_names = local.eventhub_export_log_analytics_table_names

  enable_dns_zone      = local.enable_dns_zone
  dns_zone_domain_name = local.dns_zone_domain_name
  dns_ns_records       = local.dns_ns_records
  dns_txt_records      = local.dns_txt_records
  dns_a_records        = local.dns_a_records
  dns_mx_records       = local.dns_mx_records

  enable_cdn_frontdoor                        = local.enable_cdn_frontdoor
  cdn_frontdoor_forwarding_protocol           = local.cdn_frontdoor_forwarding_protocol
  cdn_frontdoor_enable_rate_limiting          = local.cdn_frontdoor_enable_rate_limiting
  cdn_frontdoor_rate_limiting_threshold       = local.cdn_frontdoor_rate_limiting_threshold
  cdn_frontdoor_host_add_response_headers     = local.cdn_frontdoor_host_add_response_headers
  cdn_frontdoor_custom_domains                = local.cdn_frontdoor_custom_domains
  cdn_frontdoor_host_redirects                = local.cdn_frontdoor_host_redirects
  cdn_frontdoor_origin_fqdn_override          = local.cdn_frontdoor_origin_fqdn_override
  cdn_frontdoor_origin_host_header_override   = local.cdn_frontdoor_origin_host_header_override
  restrict_container_apps_to_cdn_inbound_only = local.restrict_container_apps_to_cdn_inbound_only
  container_apps_allow_ips_inbound            = local.container_apps_allow_ips_inbound
  enable_cdn_frontdoor_health_probe           = local.enable_cdn_frontdoor_health_probe

  enable_monitoring             = local.enable_monitoring
  monitor_email_receivers       = local.monitor_email_receivers
  existing_logic_app_workflow   = local.existing_logic_app_workflow
  enable_container_health_probe = local.enable_container_health_probe

  enable_container_app_blob_storage     = local.enable_container_app_blob_storage
  enable_container_app_file_share       = local.enable_container_app_file_share
  container_app_file_share_mount_path   = local.container_app_file_share_mount_path
  storage_account_ipv4_allow_list       = local.storage_account_ipv4_allow_list
  storage_account_public_access_enabled = local.storage_account_public_access_enabled

  existing_network_watcher_name                = local.existing_network_watcher_name
  existing_network_watcher_resource_group_name = local.existing_network_watcher_resource_group_name
}
