package main

import (
	"fmt"
	"html/template"
	"log"
	"net/http"
	"os"
	"time"
)

var tpl *template.Template

func init() {
	tpl = template.Must(template.ParseGlob("templates/*"))
}

func main() {
	// endpoints
	http.HandleFunc("/", index)
	http.Handle("/gallery/", http.StripPrefix("/gallery", http.FileServer(http.Dir("./gallery"))))
	// Running on...
	fmt.Println("The Celestial Beyonds - Photo Gallery \nRan @", time.Now())
	port := os.Getenv("PORT") // heroku
	//port := "8080" // local
	err := http.ListenAndServe(":"+port, nil)
	if err != nil {
		return
	}
}

// Index
func index(w http.ResponseWriter, _ *http.Request) {
	// open dir
	file, err := os.Open("gallery")
	if err != nil {
		log.Fatalf("failed opening directory: %s", err)
	}
	defer func(file *os.File) {
		err := file.Close()
		if err != nil {

		}
	}(file)

	list, _ := file.Readdirnames(0) // 0 to read all files and folders
	err = tpl.ExecuteTemplate(w, "index.gohtml", list)
	if err != nil {
		return
	}
}
