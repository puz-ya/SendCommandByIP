# SendCommandByIP.exe
Format: IP PORT "COMMAND"

## Examples
### --- --- --- UDP case

Start measurement in Continuous Mode:
>SendCommandByIP.exe 192.168.250.120 9600 "MEASURE /C"

Command [MEASURE /C] sent to
 IP address: 192.168.250.120 port 9600



Stop measurement in Continuous Mode:
>SendCommandByIP.exe 192.168.250.120 9600 "MEASURE /E"

Command [MEASURE /E] sent to
 IP address: 192.168.250.120 port 9600


### --- --- --- TCP case

Start measurement in Continuous Mode:
>SendCommandByIP.exe 192.168.250.120 9876 "MEASURE /C"

Command [MEASURE /C] sent to
 IP address: 192.168.250.120 port 9876
Sending...
Response: OK



Stop measurement in Continuous Mode:
>SendCommandByIP.exe 192.168.250.120 9876 "MEASURE /E"

Command [MEASURE /E] sent to
 IP address: 192.168.250.120 port 9876
Sending...
Response: OK
