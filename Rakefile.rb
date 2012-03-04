require 'Albacore'

task :default => [:compile, :test]

desc "Compile Solution"
msbuild :compile do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Build
  msb.verbosity = 'minimal'
  msb.solution = Dir['Source/**/*.sln'].first
end

desc "Run Tests"
mspec :test do |mspec|
  mspec.command = Dir['Source/**/mspec-clr4.exe'].first
  mspec.assemblies = Dir['Source/**/bin/**/Debug/*.Tests.dll']
end
