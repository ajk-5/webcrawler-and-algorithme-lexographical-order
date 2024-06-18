<?php

class WebCrawler {
    private $maxDepth;
    private $emails;
    private $visitedUrls;

    public function __construct($maxDepth = 2) {
        $this->maxDepth = $maxDepth;
        $this->emails = [];
        $this->visitedUrls = [];
    }

    public function crawl($url, $depth = 0) {
        if ($depth > $this->maxDepth || isset($this->visitedUrls[$url])) {
            return;
        }

        $this->visitedUrls[$url] = true;
        $html = $this->getHtml($url);

        if ($html === false) {
            return;
        }

        $this->extractEmails($html);
        $links = $this->extractLinks($html, $url);

        foreach ($links as $link) {
            $this->crawl($link, $depth + 1); // utilisition de RECURSIVITE
        }
    }

    private function getHtml($url) {
        $options = [
            "http" => [
                "method" => "GET",
                "header" => "User-Agent: web-crawler/1.0\r\n"
            ]
        ];
        $context = stream_context_create($options);
        return @file_get_contents($url, false, $context);
    }

    private function extractEmails($html) {
        $pattern = '/mailto:([a-zA-Z0-9]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})/'; //normal e-mail pattern
        preg_match_all($pattern, $html, $matches);

        foreach ($matches[1] as $email) {
            if (!in_array($email, $this->emails)) {
                $this->emails[] = $email;
            }
        }
    }

    private function extractLinks($html, $baseUrl) {
        $dom = new DOMDocument;
        @$dom->loadHTML($html);
        $links = [];
        foreach ($dom->getElementsByTagName('a') as $node) {
            $href = $node->getAttribute('href');
            if ($href && !preg_match('/^mailto:/', $href)) {
                $links[] = $this->resolveUrl($href, $baseUrl);
            }
        }
        return array_unique($links);
    }

    private function resolveUrl($href, $baseUrl) {
        if (parse_url($href, PHP_URL_SCHEME) != '') {
            return $href;
        }
        if ($href[0] == '/') {
            $baseUrl = parse_url($baseUrl);
            return $baseUrl['scheme'] . '://' . $baseUrl['host'] . $href;
        }
        $path = parse_url($baseUrl, PHP_URL_PATH);
        $path = preg_replace('#/[^/]*$#', '/', $path);
        return $baseUrl . $path . $href;
    }

    public function getEmails() {
        return $this->emails;
    }
}

// Utilisation du web crawler
$crawler = new WebCrawler(3);
$crawler->crawl('https://www.esiea.fr/vie-etudiante/');//notre ecole
$emails = $crawler->getEmails();

echo "Emails trouvés:\n";
foreach ($emails as $email) {
    echo $email . "\n";
}
?>