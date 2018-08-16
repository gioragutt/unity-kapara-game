set -ex

CURRENT_PATH=$(dirname $0)
cd $CURRENT_PATH/../Windows/x86/
mkdir tmp
for file in $(ls | grep -v Kapara.exe | grep -v tmp);
do
	mv $file tmp
done
mv tmp Kapara_Data
	
