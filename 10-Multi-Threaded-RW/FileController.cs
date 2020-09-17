using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Multi_Threaded_RW
{
    // a controller for a sequential text file:
    // allows a thread to read or write the file
    public class FileController
    {
        private File thefile;  // the file controlled by this controller

        // I.  DECLARE AND USE A STATE VARIABLE THAT REMEMBERS STATE OF thefile's USE
        private Status fileStatus = Status.Closed; 
        // II. ADD CODE TO PREVENT TWO THREADS FROM OPENING THE FILE AT THE SAME INSTANT.

        public FileController(File f) { thefile = f; }

        // opens the file for read use; returns handle to file.  
        // If file cannot be opened, returns null.
        public Reader openRead()
        {
            lock (this)
            {
                Reader r = null;
                if (fileStatus == Status.Closed)
                {
                    thefile.initRead();
                    r = thefile;
                    fileStatus = Status.Reading;
                }

                return r;
            }
        }

        // opens the file for write use; returns handle to file.  
        //   If file cannot be opened, returns null.
        public Writer openWrite()
        {
            lock (this)
            {

                Writer w = thefile;
                if (fileStatus == Status.Reading)
                {

                    thefile.initWrite();
                    w = thefile;
                    fileStatus = Status.Writing;
                }
                return w;
            }
        }

        // closes file
        public void close()
        {

        }
    }
}
