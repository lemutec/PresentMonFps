#include <spdlog\spdlog.h>
#include <spdlog\sinks\stdout_sinks.h>

std::shared_ptr<spdlog::logger> g_InspectorLogger = spdlog::stderr_logger_mt("eventLogger");
